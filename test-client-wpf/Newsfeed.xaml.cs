using Newtonsoft.Json;
using SocialNetwork2.Request;
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
            if (ClientInfo.Instance.sessionToken == null) return;


            try
            {
                LoadNewsfeedRequest request = new LoadNewsfeedRequest()
                {
                    userToken = ClientInfo.Instance.sessionToken,
                    interval = this.interval
                };

                ClientInfo.Instance.newsfeedPostList.AddRange(new ServiceConnector().loadNewsfeed(request));

                 if (ClientInfo.Instance.newsfeedPostList != null  && ClientInfo.Instance.newsfeedPostList.Any() 
                    && ClientInfo.Instance.newsfeedPostList.ElementAt(0).postContent.Length > 0)
                 {
                     foreach (var post in ClientInfo.Instance.newsfeedPostList)
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
                     throw new Exception("No more Post");
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
