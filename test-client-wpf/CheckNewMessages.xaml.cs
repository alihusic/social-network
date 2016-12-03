using Newtonsoft.Json;
using SocialNetwork.Model;
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

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for CheckNewMessages.xaml
    /// </summary>
    public partial class CheckNewMessages : Page
    {
        public CheckNewMessages()
        {
            InitializeComponent();
        }

        private void loadNewMessages(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;
            try
            {
                CheckNewMessagesQuery query = new CheckNewMessagesQuery
                {
                    userToken = ControlGroup.userToken
                };

                string urlPath = "http://localhost:51980/chat/new_messages";
                var request = (HttpWebRequest)WebRequest.Create(urlPath);
                request.Accept = "application/json";
                request.ContentType = "application/json";
                string requestBody = JsonConvert.SerializeObject(query);

                var data = Encoding.ASCII.GetBytes(requestBody);

                request.Method = "POST";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();

                List<PrivateMessages> listMessages = JsonConvert.DeserializeObject<List<PrivateMessages>>(responseString);
                if(listMessages!=null && listMessages.Any() && listMessages.ElementAt(0).messageText.Length > 0)
                {
                    foreach(var message in listMessages)
                    {
                        newMessageContent.Text += "\n";
                        newMessageContent.Text += message.senderId;
                        newMessageContent.Text += "\n";
                        newMessageContent.Text += message.messageText;
                    }
                    return;
                }
                newMessageContent.Text += responseString;
            }
            catch(Exception ex)
            {

            }
        }
        public class CheckNewMessagesQuery
        {
            public Token userToken { get; set; }
        }
    }
}
