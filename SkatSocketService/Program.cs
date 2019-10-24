using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpSkat;

namespace SkatSocketService
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipEndPoint = IpEndPointService.SetIp("127.0.0.1", 6789);
            TcpListener serverSocket = new TcpListener(ipEndPoint);

            serverSocket.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server aktiveret");
                
                AfgiftService service = new AfgiftService(connectionSocket);

                // Sørger for at hver klient får sin egen task at operere i, dette tillader flere forbindelser på samme tid
                Task.Factory.StartNew(() => service.DoIt());
            }

        }

    }
}
