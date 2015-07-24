using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    static class WebSockets
    {
        #region WebSocketReaderStream
        /// <summary>
        /// Represents a stream that reads a single message from a websocket.
        /// </summary>
        private class WebSocketReaderStream : Stream
        {
            private readonly WebSocket _webSocket;
            private readonly ArraySegment<byte> _readBuffer;
            private WebSocketReceiveResult _receiveResult;
            private int _position;

            /// <summary>
            /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
            /// </summary>
            public override bool CanRead
            {
                get { return true; }
            }

            /// <summary>
            /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
            /// </summary>
            public override bool CanSeek
            {
                get { return false; }
            }

            /// <summary>
            /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
            /// </summary>
            public override bool CanWrite
            {
                get { return false; }
            }

            /// <summary>
            /// When overridden in a derived class, gets the length in bytes of the stream.
            /// </summary>
            public override long Length
            {
                get { return 0; }
            }

            /// <summary>
            /// When overridden in a derived class, gets or sets the position within the current stream.
            /// </summary>
            /// <exception cref="System.NotSupportedException"></exception>
            public override long Position
            {
                get { return 0; }
                set { throw new NotSupportedException(); }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="WebSocketReaderStream"/> class.
            /// </summary>
            public WebSocketReaderStream(WebSocket webSocket, ArraySegment<byte> readBuffer)
            {
                _webSocket = webSocket;
                _readBuffer = readBuffer;
            }

            /// <summary>
            /// Asynchronously depletes the message.
            /// </summary>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
            /// <returns>
            /// A <see cref="Task"/> that represents the asynchronous deplete operation.
            /// </returns>
            public async Task DepleteAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                _position = -1;
                while (_receiveResult == null || !_receiveResult.EndOfMessage)
                    _receiveResult = await _webSocket.ReceiveAsync(_readBuffer, cancellationToken);
            }

            /// <summary>
            /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.
            /// </summary>
            /// <param name="buffer">The buffer to write the data into.</param>
            /// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
            /// <param name="count">The maximum number of bytes to read.</param>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
            /// <returns>
            /// A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.
            /// </returns>
            public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                if (_receiveResult == null || _receiveResult.Count == _position)
                {
                    if (_receiveResult != null && _receiveResult.EndOfMessage)
                        _position = -1;
                    else if (_receiveResult != null && _receiveResult.CloseStatus.HasValue && _webSocket.State == WebSocketState.Open)
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Requested to close", cancellationToken);
                    else if (_webSocket.State == WebSocketState.Open)
                    {
                        _receiveResult = await _webSocket.ReceiveAsync(_readBuffer, cancellationToken);
                        _position = 0;
                    }
                }

                if (_position < 0 || _webSocket.State != WebSocketState.Open) return 0;

                var toCopy = Math.Min(count, _receiveResult.Count - _position);
                Buffer.BlockCopy(_readBuffer.Array, _readBuffer.Offset + _position, buffer, offset, count);
                _position += toCopy;
                return toCopy;
            }

            /// <summary>
            /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
            /// </summary>
            /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
            /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
            /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
            /// <returns>
            /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
            /// </returns>
            public override int Read(byte[] buffer, int offset, int count)
            {
                return ReadAsync(buffer, offset, count).Result;
            }

            /// <summary>
            /// Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)
            /// </summary>
            /// <param name="buffer">The buffer to read the data into.</param>
            /// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data read from the stream.</param>
            /// <param name="count">The maximum number of bytes to read.</param>
            /// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
            /// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
            /// <returns>
            /// An <see cref="T:System.IAsyncResult" /> that represents the asynchronous read, which could still be pending.
            /// </returns>
            public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            {
                return ReadAsync(buffer, offset, count).AsApm(callback, state);
            }

            /// <summary>
            /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
            /// </summary>
            public override void Flush()
            {

            }

            #region Unsupported
            /// <summary>
            /// Sets the position within the current stream.
            /// </summary>
            /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
            /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
            /// <returns>
            /// The new position within the current stream.
            /// </returns>
            /// <exception cref="System.NotSupportedException"></exception>
            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Sets the length of the current stream.
            /// </summary>
            /// <param name="value">The desired length of the current stream in bytes.</param>
            /// <exception cref="System.NotSupportedException"></exception>
            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
            /// </summary>
            /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
            /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
            /// <param name="count">The number of bytes to be written to the current stream.</param>
            /// <exception cref="System.NotSupportedException"></exception>
            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }
            #endregion
        } 
        #endregion

        /// <summary>
        /// Reads messages from the specified <see cref="WebSocket"/> as a series of <see cref="Stream"/> objects.
        /// </summary>
        /// <param name="webSocket">The web socket.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">ws</exception>
        public static IEnumerable<Task<Stream>> Streams(this WebSocket webSocket)
        {
            if (webSocket == null) throw new ArgumentNullException("ws");

            WebSocketReaderStream reader = null;
            var buffer = WebSocket.CreateClientBuffer(Environment.SystemPageSize, 16);

            Func<Task<Stream>> next = async () =>
            {
                if (reader != null)
                {
                    await reader.DepleteAsync();
                    reader.Dispose();
                }
                return reader = new WebSocketReaderStream(webSocket, buffer);
            };

            while (webSocket.State == WebSocketState.Open)
            {
                yield return next();
            }
        }
    }
}
