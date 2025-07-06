using System.Text;
using System.Net.Sockets;
using System.Net;
using Common.Models.Tcp;
using Common.Interfaces;
using Common.Serializer;

namespace CacheClient.Connection
{
    internal class ServerConnection : IServerConnection
    {
        private TcpClient client;
        private IPAddress IP;
        private int port;
        private const int MESSAGE_LENGTH_HEADER_SIZE = 4;
        private Serializer _serializer;

        public ServerConnection(string IP, int port)
        {
            ArgumentNullException.ThrowIfNull(IP);

            this.port = port;
            this._serializer = new Serializer();
            Boolean isValidIP = IPAddress.TryParse(IP, out this.IP);

            if (!isValidIP)
                throw new ArgumentException($"ERROR: Incorrect IP provided -- {IP}");
        }


        public void Connect()
        {
            try
            {
                this.client = new TcpClient();
                client.Connect(IP, port);
            }
            catch (SocketException e)
            {
                Console.WriteLine($"ERROR: Failed to connect to Server -- {IP}::{port}");
                Console.WriteLine($"Error code : ${e.ErrorCode}");
            }
        }

        private static int ReadMessageLength(NetworkStream stream)
        {
            byte[] messageLengthBuffer = new byte[4];

            int _ = stream.Read(messageLengthBuffer, 0, MESSAGE_LENGTH_HEADER_SIZE);
            int messageLength = BitConverter.ToInt32(messageLengthBuffer);

            return messageLength;
        }

        public TcpResponse Recieve(NetworkStream stream)
        {
            int messageLength = ReadMessageLength(stream);
            byte[] buffer = new byte[messageLength];
            stream.Read(buffer, 0, messageLength);

            string response = Encoding.UTF8.GetString(buffer);
            TcpResponse deserializedResponse = _serializer.Deserialize<TcpResponse>(response);

            Console.WriteLine(deserializedResponse.Message);
            Console.WriteLine(deserializedResponse.IsSuccess);

            return deserializedResponse;
        }

        public TcpResponse Send(TcpRequest body)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                string serialized_body = _serializer.Serialize<TcpRequest>(body);

                byte[] messageLength = BitConverter.GetBytes(serialized_body.Length);
                byte[] messageBytes = Encoding.UTF8.GetBytes(serialized_body);
                byte[] payloadBytes = [..messageLength, ..messageBytes];

                Console.WriteLine("Sending Request ... ");

                stream.Write(payloadBytes, 0, payloadBytes.Length);

                TcpResponse response = Recieve(stream);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILED TO SEND PAYLOAD: " + e.Message);
                Console.Write(e);
                return new TcpResponse("Error: Request Failed", false);
            }
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}