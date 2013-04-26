using agsXMPP;
using agsXMPP.protocol.client;
using System;
using agsXMPP.protocol.x.muc;


namespace DotNetPlayer
{
    class DotNetBotBody
    {
        private XmppClientConnection connection;
        private string server;
        private string username;
        private string password;
        private string nickname;
        private Jid chatroom;
        private DotNetBotMind decisionStrategy;

        static void Main(string[] args)
        {
            //create a new bot with given account settings
            DotNetBotBody client = new DotNetBotBody("servername", "username", "password", "nickname");
            client.StartNewGame();
            Console.ReadLine();
        }

        private void outputError(object o, Exception error){     
            Console.WriteLine(error);
        }

        /// <summary>
        /// A new dotnet bot for playing the werewolves game.
        /// </summary>
        /// <param name="server">the xmpp server to connect to</param>
        /// <param name="username">the username of the xmpp account on that server</param>
        /// <param name="password">the password of the xmpp account of that server</param>
        /// <param name="nickname">the nickname to be used in the werewolves game</param>
        public DotNetBotBody(String server, String username, String password, String nickname)
        {
            this.server = server;
            this.username = username;
            this.password = password;
            this.nickname = nickname;
            this.decisionStrategy = new DotNetBotMind(this);
        }

        /// <summary>
        /// Start a new werewolves game.
        /// </summary>
        public void StartNewGame()
        {
            connection = new XmppClientConnection(server);
            connection.OnError += outputError;
            connection.OnMessage += receivedMessageDispatcher;
            connection.Open(username, password);
            connection.OnLogin += delegate(object o)
            {        
              //  connection.SendMyPresence();
                sendChatMessage("sww", "I want to play");
            };
                        
        }

        /// <summary>
        /// Send a chat message with given content to a given receiver
        /// </summary>
        /// <param name="receiver">the Jabber Id (JID) of the receiver of the message</param>
        /// <param name="content">the payload of the message</param>
        public void sendChatMessage(string receiver, string content)
        {
            Message m = new Message(receiver+"@"+server, MessageType.chat, content);
            connection.Send(m);
        //    Console.WriteLine("Chat message sent: " + m);
        }

        /// <summary>
        /// Send a message in the chatroom.
        /// </summary>
        /// <param name="content">the content of the message to be posted</param>
        public void sendChatRoomMessage(String content)
        {
            Message m = new Message(chatroom, MessageType.groupchat, content);
            connection.Send(m);
       //     Console.WriteLine("Chatroom message sent: " + m);
        }

        /// <summary>
        /// This method will parse incoming messages and call specific handlers.
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The messsage that was received</param>
        private void receivedMessageDispatcher(Object sender, Message message)
        {
            
            //check whether the message is an invitation to join a chatroom and invoke the strategy
            if (message.HasTag("invite", true))
            {
                chatroom = message.From;
                MucManager manager = new MucManager(connection);
                //Wait for 3 seconds
                System.Threading.Thread.Sleep(3000);
                manager.JoinRoom(chatroom, nickname, false);
                decisionStrategy.OnJoiningGame(chatroom.ToString());
            }

            else 
            {

                if (message.Body != null)
                {
                    decisionStrategy.OnMessageReceived(message);
                }


            } 
            
          

        }



    }
}

