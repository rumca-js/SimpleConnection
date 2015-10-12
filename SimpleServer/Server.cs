using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SimpleServer
{
    class Server
    {
        int myPort = 8001;
        Socket s = null;
        TcpListener myList = null;
        bool shouldFinish = false;

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public void open()
        {
            try
            {
                Console.CancelKeyPress += delegate
                {
                    cleanup();
                };

                IPAddress ipAd = IPAddress.Parse(GetLocalIPAddress() ); //use local m/c IP address, and use the same in the client

                /* Initializes the Listener */
                myList = new TcpListener(ipAd, myPort);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The simple server is running...");
                Console.WriteLine("The local End point is  :" + myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                while (true)
                {
                    if (shouldFinish)
                        return;

                    s = myList.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                    byte[] b = new byte[100];
                    int k = s.Receive(b);

                    if (k > 0)
                    {
                        Console.Write("Recieved: ");
                        for (int i = 0; i < k; i++)
                        {
                            Console.Write(Convert.ToChar(b[i]));
                        }
                        Console.WriteLine();
                    }
                }

                /*
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");
                 */
                /* clean up * /
                s.Close();
                myList.Stop();
                 */

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }	
        }

        void cleanup()
        {
            Console.WriteLine("Cleaning up");
            if (s != null)
            {
                s.Close();
                s = null;
            }
            if (myList != null)
            {
                myList.Stop();
                myList = null;
            }
            shouldFinish = true;
        }
    }
}
