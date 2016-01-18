using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;

namespace SlackBot.Tfs
{
    /// <summary>
    /// Represents the TFS chat message sink.
    /// </summary>
    public class TfsChatMessageSink : IChatMessageSink
    {
        private static Dictionary<string, string> _colors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Bug", "#F35A00" },
            { "Task", "#005AF3" },
            { "User Story", "#5A00F3" },
        };

        private static readonly Regex Pattern = new Regex("(^|\\b)tfs(?<id>[0-9]+)", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        //private TfsTeamProjectCollection _teamProjectCollection;
        //private Guid _teamProjectCollectionGuid;
        //private TeamFoundationServer _tfs;
        //private WorkItemStore _wis;
        //private TswaClientHyperlinkService _tswaHyperlink;
        private WorkItemTrackingHttpClient _witClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsChatMessageSink"/> class.
        /// </summary>
        public TfsChatMessageSink()
        {

        }

        /// <summary>
        /// Asynchronously initializes the sink.
        /// </summary>
        /// <param name="name">The configuration name of the sink.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="System.Threading.CancellationToken.None" />.</param>
        /// <returns>
        /// A <see cref="Task" /> that represents the asynchronous initialize operation.
        /// </returns>
        public async Task InitializeAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Delay(0);

            var element = Configuration.TfsConfigurationSection.Current.Sinks[name];
            //_teamProjectCollection = new TfsTeamProjectCollection(new Uri(element.ProjectCollection));
            //_teamProjectCollectionGuid = _teamProjectCollection.InstanceId;
            //_teamProjectCollection.Connect(ConnectOptions.IncludeServices);
            //_wis = _teamProjectCollection.GetService<WorkItemStore>();
            //if (_wis == null)
            //{
            //    throw new Exception("_teamProjectCollection.GetService<WorkItemStore>() returned null");
            //}

            //_tswaHyperlink = _teamProjectCollection.GetService<TswaClientHyperlinkService>();

            // todo: support other auth methods, see the auth samples in https://www.visualstudio.com/en-us/integrate/get-started/client-libraries/samples
            //var vssCredentials = new VssCredentials(); // Active directory auth - NTLM against a Team Foundation Server
            var vssCredentials = new VssClientCredentials(); // Visual Studio sign-in prompt
            var connection = new VssConnection(new Uri(element.ProjectCollection), vssCredentials);
            _witClient = connection.GetClient<WorkItemTrackingHttpClient>();
        }

        /// <summary>
        /// Asynchronously processes a message.
        /// </summary>
        /// <param name="slack">The slack.</param>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="System.Threading.CancellationToken.None" />.</param>
        /// <returns>
        /// A <see cref="Task{ChatMessageSinkResult}" /> that represents the asynchronous process operation.
        /// </returns>
        public async Task<ChatMessageSinkResult> ProcessMessageAsync(ISlack slack, Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_witClient == null)
            {
                throw new InvalidOperationException("WorkItem Client is null");
            }

            await Task.Delay(0);

            var attachments = new List<Attachment>();
            var matches = Pattern.Matches(message.Text);
            foreach (var id in matches.OfType<Match>().Where(x => x.Success).Select(x => x.Groups["id"]).Where(x => x.Success).Select(x => x.Value))
            {
                WorkItem wi;
                try
                {
                    // todo: use get all at once
                    wi = await _witClient.GetWorkItemAsync(XmlConvert.ToInt32(id));

                }
                catch (VssServiceException ex)
                {
                    // Ignore items that don't exist
                    // "TF401232: Work item 1 does not exist, or you do not have permissions to read it."
                    if (ex.Message.StartsWith("TF401232:"))
                    {
                        // todo: show in slack that it's not found
                        continue;
                    }
                    throw;
                }


                var fields = new List<AttachmentField>()
                    {
                        new AttachmentField("Assigned To", Convert.ToString(wi.Fields["AssignedTo"]), true),
                        new AttachmentField("State", wi.Fields["State"].ToString(), true),
                    };

                var link = wi.Url;
                var wiType = wi.Fields["System.Type"].ToString();
                var wiTitle = wi.Fields["System.Title"].ToString();
                string color;
                _colors.TryGetValue(wiType, out color);

                attachments.Add(new Attachment(string.Format("{0} {1}: {2}", wiType, wi.Id, link), color,
                    text: string.Format("<{0}|{1} {2} {3}>", SlackEscape(link), SlackEscape(wiType), id, SlackEscape(wiTitle)),
                    fields: fields));
            }

            if (attachments.Any())
            {
                await slack.SendAsync(message.CreateReply("", attachments));
                return ChatMessageSinkResult.Complete;
            }

            return ChatMessageSinkResult.Continue;
        }

        private static string SlackEscape(string val)
        {
            if (string.IsNullOrEmpty(val)) return val;

            var result = new StringBuilder(val.Length);
            for (var i = 0; i < val.Length; i++)
            {
                var c = val[i];
                switch (c)
                {
                    case '&': result.Append("&amp;"); break;
                    case '<': result.Append("&lt;"); break;
                    case '>': result.Append("&gt;"); break;
                    default: result.Append(c); break;
                }
            }

            return result.ToString();
        }
    }
}
