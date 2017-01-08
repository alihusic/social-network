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
            if (ClientInfo.Instance.SessionToken == null) return;


            try
            {
                AddFriendRequest request = new AddFriendRequest()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ClientInfo.Instance.SessionToken
                };


                friendsTextBox.Text = new ServiceConnector().addFriend(request);

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }



        private void loadFriends(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;


            try
            {
                ConfidentialRequest request = new ConfidentialRequest()
                {
                    userToken = ClientInfo.Instance.SessionToken
                };

                ClientInfo.Instance.FriendsThumbList = new ServiceConnector().getAllFriendsInfo(request);
                foreach (var friend in ClientInfo.Instance.FriendsThumbList)
                {
                    friendsTextBox.Text += "\nname: " + friend.name;
                    friendsTextBox.Text += "\n";
                    friendsTextBox.Text += "last name: " + friend.lastName;
                }

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                friendsTextBox.Text += ex.StackTrace;
            }
        }

        private void confirmFriendship(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;


            try
            {
                ConfirmFriendRequest request = new ConfirmFriendRequest()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ClientInfo.Instance.SessionToken
                };

                friendsTextBox.Text = new ServiceConnector().confirmFriend(request);
            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }

        private void removeFriendship(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;


            try
            {
                DeleteFriendRequest request = new DeleteFriendRequest()
                {
                    senderId = Int32.Parse(senderId.Text),
                    receiverId = Int32.Parse(receiverId.Text),
                    userToken = ClientInfo.Instance.SessionToken
                };

                friendsTextBox.Text = new ServiceConnector().removeFriend(request);

            }
            catch (Exception ex)
            {
                friendsTextBox.Text += ex.Message;
                return;
            }
        }
    }
}
