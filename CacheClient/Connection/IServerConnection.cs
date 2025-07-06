using System.Net.Sockets;
using Common.Models.Tcp;

namespace CacheClient.Connection
{
    internal interface IServerConnection
    {
        public void Connect();
        public TcpResponse Send(TcpRequest body);
        public TcpResponse Recieve(NetworkStream stream);
        public void Disconnect();
    }
}