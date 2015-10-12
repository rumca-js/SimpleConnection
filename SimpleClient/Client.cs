using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;

namespace SimpleClient
{
    class Client
    {
        static int myPort = 8001;

        public void send(String host, String text)
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Simple client connecting to: '" + host + "'");

                tcpclnt.Connect(host, myPort); // use the ipaddress as in the server program

                Console.WriteLine("Simple client connected");

                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(text);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                tcpclnt.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("Could not send data over the socket");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:\n" + e.GetType().Name + "\n" + e.Message + "\n" + e.StackTrace + "\n");
            }
        }

        public void sendAll(string[] hosts, String text)
        {
            foreach (String host in hosts)
            {
                String inner = host.Trim();
                if (inner != "")
                {
                    send(inner, text);
                }
            }
        }

        public string[] readReceipients()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("tolist.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();

                    return line.Split('\n');
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
