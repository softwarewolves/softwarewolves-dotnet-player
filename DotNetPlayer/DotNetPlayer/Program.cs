using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agsXMPP;
using agsXMPP.Xml;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;


namespace DotNetPlayer
{
    class DotNetClient
    {
        private XmppClientConnection connection;

        static void Main(string[] args)
        {     
            DotNetClient client = new DotNetClient();
            client.Login("jabber.org", "testvillager", "villager");
          //  System.Threading.Thread.Sleep(8000);
            Console.ReadLine();
        }


        public void Login(String server, String user, String password)
        {
            connection = new XmppClientConnection(server);
            connection.Open(user, password);
            connection.OnLogin += delegate(object o)
            {
                sendChatMessage("sww@jabber.org", "I want to play");
            };
            connection.OnMessage += receiveMessage;
        }

        public void sendChatMessage(String receiver, String content)
        {
            connection.Send(new Message(receiver, MessageType.chat, content));
           
        }

        public void joinRoom(Jid chatroom, String content) {
            Message m = new Message(chatroom, MessageType.groupchat, content);
            connection.Send(m);
            Console.WriteLine("Yeee room joined: " + chatroom);
        }

        private void receiveMessage(Object sender, Message message)
        {
            Jid chatroom = message.From;
            Console.WriteLine("Yeee message received: "+chatroom);
        }



    }
}

