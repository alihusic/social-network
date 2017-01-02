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
    /// Interaction logic for CreatePost.xaml
    /// </summary>
    public partial class CreatePost : Page
    {
        public CreatePost()
        {
            InitializeComponent();
        }

        private void post(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.sessionToken == null) return;

            try
            {
                PostCreateRequest request = new PostCreateRequest
                {
                    userToken = ClientInfo.Instance.sessionToken,
                    targetId = Int32.Parse(targetTextBox.Text),
                    creatorId = ClientInfo.Instance.sessionToken.userId,
                    postContent = postContent.Text
                };

                if (new ServiceConnector().createPost(request)) postContent.Text = "Post successfully created.";
                else postContent.Text = "Post not created successfully.";

            }
            catch (Exception ex)
            {
                postContent.Text = ex.Message;
            }
        }
    }
}
