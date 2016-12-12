using Newtonsoft.Json;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
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
using TestClientSN.Model;
using testClientWPF;

namespace TestClientSN
{
    /// <summary>
    /// Interaction logic for FriendsFunctions.xaml
    /// </summary>
    public partial class FriendsFunctions : Page
    {
        public FriendsFunctions()
        {
            InitializeComponent();
        }

        private void createFriendship(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;


            try
            {
                AddQuery query = new AddQuery()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ControlGroup.userToken
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/friends/add")
                    .Build();

                var responseString = request.requestFromServer();

                friendsTextBox.Text = responseString;

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }



        private void loadFriends(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;


            try
            {
                GetAllQuery query = new GetAllQuery()
                {
                    userToken = ControlGroup.userToken
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/friends/get_all")
                    .Build();

                var responseString = request.requestFromServer();

                List<UserFriendsInfo> listFriends = JsonConvert.DeserializeObject<List<UserFriendsInfo>>(responseString);

                if (listFriends != null && listFriends.Any() && listFriends.ElementAt(0).name.Length > 0)
                {
                    foreach (var friend in listFriends)
                    {
                        friendsTextBox.Text += "\nname: " + friend.name;
                        friendsTextBox.Text += "\n";
                        friendsTextBox.Text += "last name: " + friend.lastName;
                    }
                    return;
                }
                else
                {
                    throw new Exception("No more posts");
                }

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }

        private void confirmFriendship(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;


            try
            {
                ConfirmQuery query = new ConfirmQuery()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ControlGroup.userToken
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/friends/confirm")
                    .Build();

                var responseString = request.requestFromServer();

                friendsTextBox.Text = responseString;
            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }

        private void removeFriendship(object sender, RoutedEventArgs e)
        {
            if (ControlGroup.userToken == null) return;


            try
            {
                DeleteQuery query = new DeleteQuery()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ControlGroup.userToken
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/friends/remove")
                    .Build();

                var responseString = request.requestFromServer();

                friendsTextBox.Text = responseString;

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }
    }
}
