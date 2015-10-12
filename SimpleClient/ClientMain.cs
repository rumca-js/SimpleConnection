using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;


namespace SimpleClient
{
    class ClientMain
    {
        static void Main(string[] args)
        {
            String text = null;
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify text to send as the first parameter");
                System.Environment.Exit(1);
            }

            text = args[0];

            Client cl = new Client();
            string [] list = cl.readReceipients();

            if (list == null)
            {
                Console.WriteLine("Please make sure the files exists");
                System.Environment.Exit(2);
            }

            cl.sendAll(list, text);
        }
    }
}
