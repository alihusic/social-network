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
            if (ClientInfo.Instance.SessionToken == null) return;

            try
            {
                PostCreateRequest request = new PostCreateRequest
                {
                    userToken = ClientInfo.Instance.SessionToken,
                    targetId = Int32.Parse(targetTextBox.Text),
                    creatorId = ClientInfo.Instance.SessionToken.userId,
                    postContent = postContent.Text
                };


                postContent.Text = new ServiceConnector().createPost(request);

            }
            catch (Exception ex)
            {
                postContent.Text = ex.Message;
            }
        }
    }
}
