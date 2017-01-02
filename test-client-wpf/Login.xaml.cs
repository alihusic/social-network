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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            string username = this.username.Text;
            string password = this.passwordBox.Password;
            try
            {
                var query = new AuthenticateUserRequest
                {
                    username = username,
                    password = password
                };

                ClientInfo.Instance.sessionToken = new ServiceConnector().authenticate(query);

                statusLabel.Text += "Token successfully added";


                //statusLabel.Text = ControlGroup.userToken.tokenHash + "\n" + ControlGroup.userToken.userId;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.StackTrace;
            }
        }

        private void loadLogin(object sender, RoutedEventArgs e)
        {
            //probably unnecessary
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.sessionToken == null) return;

            try
            {
                ConfidentialRequest query = new ConfidentialRequest
                {
                    userToken = ClientInfo.Instance.sessionToken
                };

                

                if(new ServiceConnector().logOut(query)) ClientInfo.Instance.sessionToken = null;
                statusLabel.Text = "Logged out";
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }
    }

}
