using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ReporterWPF.Update
{
    using Reporter.Server.Handlers;
    using Reporter.Server.Handlers;
    using Reporter.Server.Utils;
    using System.Threading.Tasks;

    class ServerHandler : AutomaticHandler
    {
        private static ServerHandler INSTANCE;

        public static ServerHandler Instance
        {
            get { return INSTANCE ?? (INSTANCE = new ServerHandler()); }
        }

        private const int SLEEP_SECONDS = 10;

        private const int PORT = 12345;
        private static readonly string SERVER_ADDRESS_KEY = "ServerAddress";
        private static readonly string SERVER_ADDRESS = ConfigurationManager.AppSettings[SERVER_ADDRESS_KEY];
        private static readonly EndPoint ENDPOINT = new IPEndPoint(IPAddress.Parse(SERVER_ADDRESS), PORT);
        //private static readonly EndPoint ENDPOINT = new IPEndPoint(IPAddress.Loopback, PORT);

        private ServerHandler()
        {
            OnHandlerStopped += ServerHandlerStopped;
        }

        private Socket socket;

        protected override void Main()
        {
            using (socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                Logger.SERVER.Information("ServerHandler: Started.");

                try
                {
                    socket.Connect(ENDPOINT);
                    Logger.SERVER.Information("ServerHandler: Connected to server.");

                    var buffer = new byte[1];
                    while (socket.Connected)
                    {
                        try
                        {
                            var c = socket.Receive(buffer);
                            if (c != 0)
                            {
                                Logger.SERVER.Information("ServerHandler: Infrom recieved from server.");
                                if (DatabaseUpdated != null)
                                    DatabaseUpdated.Invoke();
                            }
                        }
                        catch (SocketException e)
                        {
                            Logger.SERVER.Information("ServerHandler: Error while socket was connected: " + e.Message +
                                                      "\n\n" + e.StackTrace);
                        }
                    }

                    socket.Close();
                    Logger.SERVER.Information("ServerHandler: Connection to server closed.");
                }
                catch (SocketException e)
                {
                    Logger.SERVER.Warning("ServerHandler: Could not connect to server: " + e.Message + "\n" +
                                          "Waiting for " + SLEEP_SECONDS + "s.\n\n" + e.StackTrace);
                    Thread.Sleep(TimeSpan.FromSeconds(SLEEP_SECONDS));
                }
                finally
                {
                    socket.Dispose();
                }
            }
        }

        public event Action DatabaseUpdated;

        public void SendCommand(byte[] data)
        {
            if (socket != null && socket.Connected)
            {
                Task.Run(delegate
                {
                    socket.Send(data);
                    Logger.SERVER.Information("ServerHandler: Command sent to server.\n" +
                                              data.Select(b => b.ToString("X2")).Aggregate((b1, b2) => b1 + "," + b2));
                });
            }
        }

        private void ServerHandlerStopped(HandlerManagementOptions options)
        {
            Logger.SERVER.Warning("ServerHandler: Stopped"
                                  + (options.Exception == null ? "!" : ": " + options.Exception.Message));
            options.Restart = true;
        }

        protected override void OnStop()
        {
            socket.Dispose();
        }
    }
}
