using SlackBot.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ParentProcess.RunningAsService)
                ServiceBase.Run(new SlackBotService());
            else
            {
                using (var service = new SlackBotService())
                {
                    service.ConsoleStart(args);
                    while (char.IsControl(Console.ReadKey(true).KeyChar)) ;
                    service.Stop();
                }
            }
        }
    }
}
