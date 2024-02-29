using Microsoft.AspNetCore.SignalR;

namespace ChatApplication_backend.Hub
{
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
        public static List<Message> Messages;

        public Chat()
        {
            if(Messages == null)
                Messages = new List<Message>();
        }
        public void NewMessage(string username, string text)
        {
            Clients.All.SendAsync("newMessage", username, text);
            Messages.Add(new Message()
            {
                Text = text,
                UserName = username
            });
        }

        public void NewUser(string username, string connectionId)
        {
            Clients.Client(connectionId).SendAsync("previousMessages", Messages);
            Clients.All.SendAsync("newUser", username);
        }
    }

    public class Message 
    {
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
