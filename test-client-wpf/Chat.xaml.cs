using SocialNetwork2.Request;
using SocialNetworkServer;
using System;
using System.Windows;
using System.Windows.Controls;
using testClientWPF;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        public Chat()
        {
            InitializeComponent();
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            
            if (ClientInfo.Instance.sessionToken == null) return;
            try
            {
                MessageSendRequest request = new MessageSendRequest
                {
                    userToken = ClientInfo.Instance.sessionToken,
                    receiverId = Int32.Parse(chatId.Text),
                    messageText = messageContent.Text
                };

                if (new ServiceConnector().sendMessage(request)) messageContent.Text = "Message sent successfully.";
                else messageContent.Text = "Message not sent.";
            }
            catch(Exception ex) { }
            
        }


        private void loadChat(object sender, RoutedEventArgs e)
        {

        }
    }
}
