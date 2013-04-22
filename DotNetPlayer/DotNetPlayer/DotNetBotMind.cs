using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agsXMPP.protocol.client;

namespace DotNetPlayer
{
    class DotNetBotMind
    {

        private DotNetBotBody dotnetbot;

        /// <summary>
        /// Triggered when a message is received.
        /// </summary>
        /// <param name="content"></param>
        public void OnMessageReceived(Message message)
        {
            Console.Out.WriteLine("Message received from "+message.From.ToString()+": "+message.Body.ToString());
        }


        /// <summary>
        /// Triggered when joining a chatroom.
        /// </summary>
        /// <param name="chatroom">the JID (Jabber Id) the chatroom</param>
        public void OnJoiningGame(string chatroom)
        {
            Console.Out.WriteLine("Joined chatroom " + chatroom);

        }


        /// <summary>
        /// A new mind that controls the given dotnetbotbody
        /// </summary>
        /// <param name="bot">the body that is the context for this strategy</param>
        public DotNetBotMind(DotNetBotBody bot)
        {
            this.dotnetbot = bot;
        }

    }
}
