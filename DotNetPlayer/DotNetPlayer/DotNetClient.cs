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
        private string username;
        private string password;
        private string nickname;
        private Jid chatroom;
        private DecisionStrategy decisionStrategy;

        static void Main(string[] args)
        {
            DotNetClient client = new DotNetClient(null, "awtest1.vm.bytemark.co.uk", "testvillager", "villager1", "AngryVillager");
            client.StartNewGame();
            Console.ReadLine();
        }

        private void outputError(object o, Exception error){     
            Console.WriteLine(error);
        }

        public DotNetClient(DecisionStrategy strat, String server, String username, String password, String nickname)
        {
            this.server = server;
            this.username = username;
            this.password = password;
            this.nickname = nickname;
            this.decisionStrategy = strat;
        }

        public void StartNewGame()
        {
            connection = new XmppClientConnection(server);
            connection.OnError += outputError;
            connection.OnMessage += receiveMessage;
            connection.Open(username, password);
            connection.OnLogin += delegate(object o)
            {        
              //  connection.SendMyPresence();
                sendChatMessage("sww@"+server, "I want to play");
            };
                        
        }

        public void sendChatMessage(String receiverJID, String content)
        {
            Message m = new Message(receiverJID, MessageType.chat, content);
            connection.Send(m);
            Console.WriteLine("Chat message sent: " + m);
        }

        public void sendChatRoomMessage(String content)
        {
            Message m = new Message(chatroom, MessageType.groupchat, content);
            connection.Send(m);
            Console.WriteLine("Chatroom message sent: " + m);
        }


        private void receiveMessage(Object sender, Message message)
        {
            Console.WriteLine("Message received: " + message);

            //detect of the message is an invitation to a chatroom
            if (message.HasTag("invite", true))
            {
                Console.WriteLine("Message interpreted as invitation");
                chatroom = message.From;
                MucManager manager = new MucManager(connection);
                System.Threading.Thread.Sleep(3000);
                manager.JoinRoom(chatroom, nickname, false);
            }
            else if(message.Body!=null && message.Body.ToString().StartsWith("Please vote who should be hanged"))
            {
                sendChatRoomMessage("I vote for JoligeHeidi");
                Console.WriteLine("Voted for JoligeHeidi");


            } else
            {
                Console.WriteLine("Message not interpreted");
            }
            
           
          

        }



    }
}

