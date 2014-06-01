using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using VhdGamer.Communication;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Lobby localLobby = new Lobby();
            localLobby.OnMessageReveived += delegate(object o, MessageEventArgs e) {
                Console.WriteLine("RX: " + ((ChatMessage)e.Message).Text);
            };
            localLobby.Listen();

            Thread.Sleep(2000);

            localLobby.Send(new ChatMessage("Hallo"));

            Thread.Sleep(2000);

            localLobby.StopListening();

            //Lobby externalLobby = new Lobby(54321, 12345);
            //externalLobby.OnMessageSent += delegate(object o, MessageEventArgs e) {
            //    Console.WriteLine("TX: " + ((ChatMessage)e.Message).Text);
            //};
            //externalLobby.Send(new ChatMessage("Hallo"));

        }
    }
}
