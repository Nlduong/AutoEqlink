using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using System.Net.Sockets;
using System.Windows;
using System.Threading;

namespace AutoCapture.DAL
{

    class NodeSocket
    {
        public TcpClient tcpClient;
        public NetworkStream serverStream;
        string readData;// = string.Empty;
        string msg;// = "Conected to Chat Server ...";
        public string Host {get;set;}
        public int Port { get; set;}
        public string res { get; set; }
        public NodeSocket()
        {
            this.Host = "localhost";
            this.Port = 8000;
            tcpClient = new TcpClient();
            serverStream = default(NetworkStream);
            
        }
        public NodeSocket(string host, int port)
        {
            this.Host = host;
            this.Port = port;
            tcpClient = new TcpClient();
            serverStream = default(NetworkStream);

        }

        public void Connect(out string err)
        {
            err = string.Empty;
            try
            {
                if (this.Port == null)
                {
                    this.Port = 8000;
                }

                if (string.IsNullOrEmpty(this.Host))
                {
                    tcpClient.Connect("127.0.0.1", this.Port);
                    serverStream = tcpClient.GetStream();
                }
                else
                {
                    tcpClient.Connect(this.Host, this.Port);
                    serverStream = tcpClient.GetStream();
                }
                res = "";
                // upload as javascript blob
                Task taskOpenEndpoint = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        // Read bytes
                        serverStream = tcpClient.GetStream();
                        byte[] message = new byte[4096];
                        int bytesRead;
                        bytesRead = 0;

                        try
                        {
                            // Read up to 4096 bytes
                            bytesRead = serverStream.Read(message, 0, 4096);
                        }
                        catch
                        {
                            /*a socket error has occured*/
                        }

                        //We have rad the message.
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        // Update main window
                        res = encoder.GetString(message, 0, bytesRead);
                        Thread.Sleep(500);
                    }
                });
                //return res;
                err = "";
            }
            catch(Exception e)
            {
                err = e.Message;
            }
        }

        public void SendMessage(string ms)
        {
            byte[] outStream = Encoding.ASCII.GetBytes(ms);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }


        public string ReadData()
        {
            return res;
        }

        
    }
}
