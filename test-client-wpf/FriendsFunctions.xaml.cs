using Newtonsoft.Json;
using SocialNetwork;
using SocialNetwork2.Request;
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
            if (ClientInfo.Instance.sessionToken == null) return;


            try
            {
                AddFriendRequest request = new AddFriendRequest()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ClientInfo.Instance.sessionToken
                };



                if (new ServiceConnector().addFriend(request)) friendsTextBox.Text = "Friend successfully added.";
                else friendsTextBox.Text = "Friend not added.";

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }



        private void loadFriends(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.sessionToken == null) return;


            try
            {
                ConfidentialRequest request = new ConfidentialRequest()
                {
                    userToken = ClientInfo.Instance.sessionToken
                };

                ClientInfo.Instance.friendsThumbList = new ServiceConnector().getAllFriendsInfo(request);

                if (ClientInfo.Instance.friendsThumbList != null && ClientInfo.Instance.friendsThumbList.Any() 
                    && ClientInfo.Instance.friendsThumbList.ElementAt(0).name.Length > 0)
                {
                    foreach (var friend in ClientInfo.Instance.friendsThumbList)
                    {
                        friendsTextBox.Text += "\nname: " + friend.name;
                        friendsTextBox.Text += "\n";
                        friendsTextBox.Text += "last name: " + friend.lastName;
                    }
                    return;
                }
                else
                {
                    throw new Exception("No more Post");
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
            if (ClientInfo.Instance.sessionToken == null) return;


            try
            {
                ConfirmFriendRequest request = new ConfirmFriendRequest()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ClientInfo.Instance.sessionToken
                };

                if (new ServiceConnector().confirmFriend(request)) friendsTextBox.Text = "Friendship successfully confirmed.";
                else friendsTextBox.Text ="Something went wrong";
            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }

        private void removeFriendship(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.sessionToken == null) return;


            try
            {
                DeleteFriendRequest request = new DeleteFriendRequest()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ClientInfo.Instance.sessionToken
                };


                if (new ServiceConnector().removeFriend(request)) friendsTextBox.Text = "Friend successfully removed.";
                else friendsTextBox.Text = "Something went wrong.";

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }
    }
}
