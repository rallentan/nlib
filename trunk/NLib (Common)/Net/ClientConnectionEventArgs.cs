using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;

namespace NLib.Net
{
    public class ClientConnectionEventArgs
    {
        //--- Fields ---

        SslStream _sslStream = null;

        //--- Constructors ---

        public ClientConnectionEventArgs(TcpClient client)
        {
            Client = client;
        }

        //--- Properties ---

        public SslStream SslStream
        {
            get
            {
                if (_sslStream == null)
                    _sslStream = new SslStream(Client.GetStream(), false);
                return _sslStream;
            }
        }

        public TcpClient Client { get; private set; }
    }

    public delegate void ClientConnectionEventHandler(object sender, ClientConnectionEventArgs e);
}
