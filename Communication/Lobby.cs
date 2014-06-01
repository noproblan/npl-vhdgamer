using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace VhdGamer.Communication
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(Message m)
        {
            Message = m;
        }
        public Message Message { get; set; }
    }

    public class Lobby
    {
        private const int BUFFER_LENGTH = 2048;
        private const int TTL = 10;
        private IPAddress multicastAddress = IPAddress.Parse("224." + (int)'n' + "." + (int)'p' + "." + (int)'l');
        private int listeningPort = 7489;
        private Socket listeningSocket;

        public Lobby(int listeningPort = 0)
        {
            if (listeningPort != 0)
            {
                this.listeningPort = listeningPort;
            }

            // We want to reuse this socket
            listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        ~Lobby()
        {
            StopListening();
        }

        public void Send(Message msg)
        {
            MemoryStream s = new MemoryStream();
            BinaryFormatter f = new BinaryFormatter();
            f.Serialize(s, msg);
            byte[] b = s.ToArray();

            IPEndPoint ipep = new IPEndPoint(multicastAddress, listeningPort);

            // Some weird memory exception occurs if I reuse the socket
            Socket sendingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendingSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastAddress));
            sendingSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, TTL);
            sendingSocket.Connect(ipep);
            sendingSocket.Send(b, b.Length, SocketFlags.None);

            if (OnMessageSent != null)
            {
                OnMessageSent(this, new MessageEventArgs(msg));
            }

            sendingSocket.Close();
        }

        public void Listen()
        {
            Thread t = new Thread(() =>
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, listeningPort);
                listeningSocket.Bind(ipep);
                listeningSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastAddress, IPAddress.Any));

                while (listeningSocket.IsBound)
                {
                    byte[] b = new byte[BUFFER_LENGTH];
                    Console.WriteLine("Waiting for data..");
                    listeningSocket.Receive(b);

                    MemoryStream stream = new MemoryStream(b);
                    BinaryFormatter f = new BinaryFormatter();
                    stream.Position = 0;
                    try
                    {
                        Message m = (Message)f.Deserialize(stream);

                        if (OnMessageReveived != null)
                        {
                            OnMessageReveived(this, new MessageEventArgs(m));
                        }
                    }
                    catch (SerializationException e)
                    {
                        Debug.WriteLine("Got invalid message!");
                    }
                }
            });
            t.Start();
        }

        public void StopListening()
        {
            if (listeningSocket.IsBound) 
            {
                listeningSocket.Shutdown(SocketShutdown.Both);
            }
        }

        public event EventHandler<MessageEventArgs> OnMessageSent;
        public event EventHandler<MessageEventArgs> OnMessageReveived;
    }
}
