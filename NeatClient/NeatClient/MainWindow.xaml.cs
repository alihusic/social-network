using Newtonsoft.Json;
using SocialNetwork.Model;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NeatClient
{
    class ControlGroup
    {
        public static Token userToken { get; set; }
        public static HttpWebRequest formalizeRequest(HttpWebRequest request)
        {
            request.Accept = "application/json";
            request.ContentType = "application/json";
            return null;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ServerData.host = "http://localhost";
            ServerData.port = "60749";
            InitializeComponent();
        }

        private void logInButtonClick(object sender, RoutedEventArgs e)
        {
            string username = this.usernameInput.Text;
            string password = this.passwordBox.Password;

            try
            {
                var query = new AuthenticateQuery
                {
                    username = username,
                    password = password
                };

                string requestBody = JsonConvert.SerializeObject(query);
                

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/authenticate")
                    .Build();
                
                var responseString = request.requestFromServer();

                MessageBox.Show(responseString);
                var tempToken = JsonConvert.DeserializeObject<Token>(responseString);
                
                if (tempToken.tokenHash != null && tempToken.tokenHash.Length == 40)
                {
                    ControlGroup.userToken = tempToken;
                    //throw new Exception("Log In incorrect");
                }

                MessageBox.Show(responseString);
                //statusLabel.Text = ControlGroup.userToken.tokenHash + "\n" + ControlGroup.userToken.userId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /*MainWindow neat = new MainWindow();
            neat.Show();
            this.Close();*/
        }

        private void createNewAccountButtonClick(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (ControlGroup.userToken == null) return;

            try
            {
                LogOutQuery query = new LogOutQuery
                {
                    userToken = ControlGroup.userToken,
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/log_out")
                    .Build();

                var responseString = request.requestFromServer();

                ControlGroup.userToken = null;
                //statusLabel.Text = "" + responseString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
