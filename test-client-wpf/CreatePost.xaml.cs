using Newtonsoft.Json;
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
            if (ControlGroup.userToken == null) return;

            try
            {
                CreateQuery query = new CreateQuery
                {
                    userToken = ControlGroup.userToken,
                    targetId = Int32.Parse(targetTextBox.Text),
                    creatorId = ControlGroup.userToken.userId,
                    postContent = postContent.Text
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/post/create")
                    .Build();

                var responseString = request.requestFromServer();
                postContent.Text = "SENT";

            }
            catch (Exception ex)
            {
                postContent.Text = ex.Message;
            }
        }
    }
}
