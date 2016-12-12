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
<<<<<<< HEAD
        private int interval = 0;
=======
        int interval = 0;
>>>>>>> refs/remotes/origin/Maulwurf


        public Newsfeed()
        {
            InitializeComponent();
        }

<<<<<<< HEAD
        private void loadNewsfeed(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;

            try
            {
                LoadNewsfeedQuery query = new LoadNewsfeedQuery
                {
                    userToken = ControlGroup.userToken,
                    interval = interval
=======

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;


            try
            {
                LoadNewsfeedQuery query = new LoadNewsfeedQuery()
                {
                    userToken = ControlGroup.userToken,
                    interval = this.interval
>>>>>>> refs/remotes/origin/Maulwurf
                };

                string urlPath = "http://localhost:60749/newsfeed/load";
                var request = (HttpWebRequest)WebRequest.Create(urlPath);
                request.Accept = "application/json";
                request.ContentType = "application/json";
<<<<<<< HEAD
                
=======

>>>>>>> refs/remotes/origin/Maulwurf
                string requestBody = JsonConvert.SerializeObject(query);

                var data = Encoding.ASCII.GetBytes(requestBody);
                request.Method = "POST";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
<<<<<<< HEAD
                

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                

                IEnumerable<Posts> listPosts = JsonConvert.DeserializeObject<IEnumerable<Posts>>(responseString);
                newsfeedContent.Text += listPosts.First();

                if (listPosts != null && listPosts.Any() && listPosts.ElementAt(0).postContent.Length > 0)
                {
                    foreach (var post in listPosts)
                    {
                        newsfeedContent.Text += "\n";
                        newsfeedContent.Text += "creatorId: "+post.creatorId;
                        newsfeedContent.Text += "\n";
                        newsfeedContent.Text += "TargetId: " + post.targetId;
                        newsfeedContent.Text += "\n";
                        newsfeedContent.Text += post.postContent;
                    }
                    return;
                }
                
                newsfeedContent.Text += responseString;
            }
            catch (Exception ex)
            {
                newsfeedContent.Text = ex.Message;
            }
=======
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();

                //newsfeedContent.Text += responseString;
                if (responseString == null) throw new Exception("No more posts!");
                List<Posts> listPosts = JsonConvert.DeserializeObject<List<Posts>>(responseString);

                 if (listPosts != null  && listPosts.Any() && listPosts.ElementAt(0).postContent.Length > 0)
                 {
                     foreach (var post in listPosts)
                     {
                         newsfeedContent.Text += "\ncreatorId: " + post.creatorId;
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
            
>>>>>>> refs/remotes/origin/Maulwurf
        }
    }
}
