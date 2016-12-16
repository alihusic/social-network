using Newtonsoft.Json;
using SocialNetwork.Model;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            
            if (ControlGroup.userToken == null) return;
            try
            {
                MessageSendRequest query = new MessageSendRequest
                {
                    userToken = ControlGroup.userToken,
                    receiverId = Int32.Parse(chatId.Text),
                    messageText = messageContent.Text
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/chat/send_message")
                    .Build();

                var responseString = request.requestFromServer();
                messageContent.Text = "SENT";
                //messageContent.Text += responseString;
                //statusLabel.Text = responseString;

               

            }
            catch(Exception ex) { }
            
        }


        private void loadChat(object sender, RoutedEventArgs e)
        {

        }
    }
}
