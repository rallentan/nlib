// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

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
