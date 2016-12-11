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

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for Newsfeed.xaml
    /// </summary>
    public partial class Newsfeed : Page
    {
        int interval = 0;


        public Newsfeed()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;


            try
            {
                LoadNewsfeedQuery query = new LoadNewsfeedQuery()
                {
                    userToken = ControlGroup.userToken,
                    interval = interval
                };

                string urlPath = "http://localhost:60749/newsfeed/load";
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
                

                List<Posts> listPosts = JsonConvert.DeserializeObject<List<Posts>>(responseString);

                if (listPosts != null  && listPosts.Any() && listPosts.ElementAt(0).postContent.Length > 0)
                {
                    foreach (var post in listPosts)
                    {
                        newsfeedContent.Text += "creatorId: " + post.creatorId;
                        newsfeedContent.Text += "\n";
                        newsfeedContent.Text += "TargetId: " + post.targetId;
                        newsfeedContent.Text += "\n";
                        newsfeedContent.Text += "" + post.postContent;
                    }

                    interval += 10;
                    return;
                }
                else
                {
                    throw new Exception("No more posts");
                }

            }
            catch (Exception ex)
            {
                newsfeedContent.Text += ex.Message;
                return;
            }
            
        }
    }
}
