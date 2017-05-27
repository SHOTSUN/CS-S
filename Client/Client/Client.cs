using System;
using System.Net.Sockets;

namespace Client
{
    class Client
    {

        private TcpClient client = null;

        public Client() { }

        public Client(int port, String IP)
        {
            try
            {
                client = new TcpClient(IP, port);
                }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("ArgumentNullException: {0}", exception);
            }
            catch (SocketException exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }
        }
        

        public void send(String message)
        {
            try { 
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("ArgumentNullException: {0}", exception);
            }
            catch (SocketException exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }

        }

        public String receive()
        {
            String responseData = String.Empty;

            try
            {
                Byte [] data = new Byte[256];


                NetworkStream stream = client.GetStream();

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                
                stream.Close();

            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("ArgumentNullException: {0}", exception);
            }
            catch (SocketException exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }

            return responseData;
        }
        
    }


}
