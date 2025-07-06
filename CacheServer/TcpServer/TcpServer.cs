using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;
using System.Text.Json;
using Common.Models.Cache;
using Common.Models.Tcp;
using Common.Serializer;
using Common.Interfaces;
using System.Threading.Tasks;
using CacheServer.Commands;
using CacheServer.Cache;

namespace Server.TcpServer
{
    internal class TCPServer
    {

        private const int MESSAGE_LENGTH_HEADER_SIZE = 4;
        private TcpListener _tcpListener;
        public Boolean isRunning;
        private IPAddress IP;
        private int Port;
        private ISerializer _serializer;
        private ICacheStore _cache;
        private CommandFactory _factory;

        public TCPServer()
        {
            var appsetting_IP = ConfigurationManager.AppSettings["IP"] != null ? ConfigurationManager.AppSettings["IP"] : "127.0.0.1";
            var appsetting_port_str = ConfigurationManager.AppSettings["PORT"] ?? "1337";
            var appsetting_port_int = Int32.Parse(appsetting_port_str);


            this.IP = IPAddress.Parse(appsetting_IP);
            this.Port = appsetting_port_int;
            this.isRunning = false;
            this._serializer = new Serializer();
            this._cache = new CacheStore();
            this._factory = new CommandFactory(_cache);
        }

        public TCPServer(string IP, int Port)
        {
            Boolean isIPValid = IPAddress.TryParse(IP, out this.IP);

            if (!isIPValid)
            {
                throw new ArgumentException("ERROR: IP string is invalid");
            }

            this.Port = Port;
            this.isRunning = false;
            this._serializer = new Serializer();
            this._cache = new CacheStore();
            this._factory = new CommandFactory(_cache);
        }

        public TCPServer(string IP, int port, ISerializer serializer)
        {
            Boolean isIPValid = IPAddress.TryParse(IP, out this.IP);

            if (!isIPValid)
            {
                throw new ArgumentException("ERROR: IP string is invalid");
            }

            this.Port = port;
            this.isRunning = false;
            this._serializer = serializer;
            this._cache = new CacheStore();
            this._factory = new CommandFactory(_cache);
        }


        //Read the length of the content that the client will send (number of bytes)
        public int ReadMessageLength(NetworkStream stream)
        {
            byte[] messageLengthBuffer = new byte[4];

            int _ = stream.Read(messageLengthBuffer, 0, MESSAGE_LENGTH_HEADER_SIZE);
            int messageLength = BitConverter.ToInt32(messageLengthBuffer);

            return messageLength;
        }

        public TcpRequest ReadRequest(NetworkStream stream)
        {
            try
            {
                int messageLength = ReadMessageLength(stream);

                if (messageLength == 0) return null;

                byte[] messageBuffer = new byte[messageLength];
                string messageRecieved;
                int totalBytesRead = 0;

                Console.WriteLine("Message Length : " + messageLength);

                totalBytesRead = stream.Read(messageBuffer, 0, messageLength);
                messageRecieved = Encoding.UTF8.GetString(messageBuffer, 0, totalBytesRead);
                TcpRequest request = _serializer.Deserialize<TcpRequest>(messageRecieved);

                Console.WriteLine("Incoming Request : " + request.Method + "\n\n");
                return request;

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Read message from stream");
                Console.WriteLine(e);
                //dummy for now , change later   <------
                return null;
            }
        }

        public void SendResponse(NetworkStream stream, TcpResponse response)
        {
            try
            {
                string serializedResponse = _serializer.Serialize<TcpResponse>(response);

                Console.WriteLine("Sending response to client ,  message : " + response.Message);

                byte[] responseBytes = Encoding.UTF8.GetBytes(serializedResponse);
                byte[] responseLengthBytes = BitConverter.GetBytes(responseBytes.Length);
                byte[] payload = [..responseLengthBytes, ..responseBytes];

                stream.Write(payload, 0, payload.Length);
            }
            catch (Exception e)
            {
                Console.Write("Error: Failed to send response to client");
                Console.WriteLine(e);
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                Console.WriteLine("Accepted New client request");
                using NetworkStream tcpStream = client.GetStream();
                while (client.Connected)
                {
                    TcpRequest request = ReadRequest(tcpStream);

                    if (request == null) continue;

                    CommandBase command = _factory.CreateCommand(request);
                    CommandResponse response = command.Execute();
                    TcpResponse tcpResponse = new TcpResponse(response.Item, response.Message, response.IsSuccess);

                    SendResponse(tcpStream, tcpResponse);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while handling client");
                Console.WriteLine(e);
            }
            finally
            {
                client.Close();
            }
        }

        //LOOK INTO HOW YOU ARE DOING MULTI THREADING 
        public async void LoopRequests()
        {
            try
            {
                while (true)
                {
                    TcpClient client = await _tcpListener.AcceptTcpClientAsync();

                    _ = Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to accept new client request");
                Console.WriteLine(e);
            }
        }

        public void Start()
        {
            //change this later
            _tcpListener = new TcpListener(IPAddress.Any, 1337);
            _tcpListener.Start();

            Console.WriteLine("Started Server at : " + _tcpListener.LocalEndpoint);

            isRunning = true;
            LoopRequests();
        }

    }

}