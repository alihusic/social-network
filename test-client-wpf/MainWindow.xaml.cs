using Newtonsoft.Json;
using SocialNetwork2.Request;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TestClientSN;
using testClientWPF;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ServerData.host = "http://svab0ni-001-site1.etempurl.com";
            ServerData.port = "80";
            
            InitializeComponent();
        }

        private void openLogin(object sender, RoutedEventArgs e)
        {
            Main.Content = new Login();
        }

        private void openSignup(object sender, RoutedEventArgs e)
        {
            Main.Content = new Signup();
        }
        private void openChat(object sender, RoutedEventArgs e)
        {
            Main.Content = new Chat();
        }

        private void openCheckNewMessages(object sender, RoutedEventArgs e)
        {
            Main.Content = new CheckNewMessages();
        }

        private void openNewsfeed(object sender, RoutedEventArgs e)
        {
            Main.Content = new Newsfeed();
        }

        private void openViewPost(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewPost();
        }

        private void openCreatePost(object sender, RoutedEventArgs e)
        {
            Main.Content = new CreatePost();
        }


        private void openFriendsFunctions(object sender, RoutedEventArgs e)
        {
            Main.Content = new FriendsFunctions();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;

            try
            {
                ConfidentialRequest query = new ConfidentialRequest
                {
                    userToken = ClientInfo.Instance.SessionToken
                };

                if (new ServiceConnector().logOut(query)) ClientInfo.Instance.SessionToken = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}

