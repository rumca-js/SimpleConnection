using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace SimpleServer
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            Server ser = new Server();
            ser.open();

            Console.WriteLine("Test of end");
        }
    }
}
