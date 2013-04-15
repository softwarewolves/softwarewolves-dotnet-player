using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.Xml;
using agsXMPP.Xml.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agsXMPP.protocol.x.muc;


namespace DotNetPlayer
{
    class DotNetClient
    {
        private XmppClientConnection connection;
        private string server;

        static void Main(string[] args)
        {     
            DotNetClient client = new DotNetClient();
        //    Console.WriteLine(client.makeChangeRoomPresenceConfirmationMessage("testroom@jabber.org","mynickname","id12345"));

            //awtest1.vm.bytemark.co.uk
            client.Login("awtest1.vm.bytemark.co.uk", "testvillager", "villager");
          //  System.Threading.Thread.Sleep(8000);
            Console.ReadLine();
        }

        private void outputError(object o, Exception error){
        
            Console.WriteLine(error);
        }


        public void Login(String server, String user, String password)
        {
            connection = new XmppClientConnection(server);
            connection.OnError += outputError;
            connection.OnMessage += receiveMessage;
            connection.Open(user, password);
            connection.OnLogin += delegate(object o)
            {
                
                connection.SendMyPresence();
                sendChatMessage("sww@"+server, "I want to play");
            };
            
            
        }

        public void sendChatMessage(String receiver, String content)
        {
            Message m = new Message(receiver, MessageType.chat, content);
            connection.Send(m);
            Console.WriteLine("Chat message sent: " + m);
        }

        public void sendChatRoomMessage(String room, String content)
        {
            Message m = new Message(room + "/testvillager", MessageType.groupchat, content);

            connection.Send(m);
            Console.WriteLine("Chatroom message sent: " + m);
        }

        public void sendMessage(Element e) {
            connection.Send(e);
            Console.WriteLine("Custom message sent: " + e.ToString());
        }

        private Element makeChangeRoomPresenceConfirmationMessage(String room, String nickname, String id)
        {
            Element result = new Element();
            result.TagName = "presence";
            result.SetAttribute("id", id);
            result.SetAttribute("to", room + "/" + nickname);

            Element x = new Element();
            x.TagName = "x";
            x.Namespace = "http://jabber.org/protocol/muc";
            result.AddChild(x);

            Element password = new Element();
            password.TagName = "password";
            x.AddChild(password);

            Element history = new Element();
            history.TagName = "history";
            history.SetAttribute("maxstanzas", "0");
            x.AddChild(history);

            return result;
        }

        private void receiveMessage(Object sender, Message message)
        {
            Console.WriteLine("Message received: " + message);

            string chatroom=" ";
            //detect of the message is an invitation to a chatroom
            if (message.HasTag("invite", true))
            {
                chatroom = message.From.ToString();
                Jid chatroomJid = message.From;
                
                Console.WriteLine("Message interpreted as invitation");
                MucManager manager = new MucManager(connection);
                manager.JoinRoom(chatroomJid, "mynickname", false);
                //sendMessage(makeChangeRoomPresenceConfirmationMessage(chatroom,"mynickname","id1"));
                
            }
            else 
            {
                Console.WriteLine("Message not interpreted");
            }
            
           
          

        }



    }
}

