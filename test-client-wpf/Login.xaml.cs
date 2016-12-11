using Newtonsoft.Json;
using SocialNetwork.Model;
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

namespace SocialNetwork
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
                var authenticateQuery = new AuthenticateQuery
                {
                    username = username,
                    password = password
                };

                string urlPath = "http://localhost:60749/user/authenticate";
                var request = (HttpWebRequest)WebRequest.Create(urlPath);
                request.Accept = "application/json";
                request.ContentType = "application/json";
                string requestBody = JsonConvert.SerializeObject(authenticateQuery);

                var data = Encoding.ASCII.GetBytes(requestBody);

                request.Method = "POST";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                statusLabel.Text = responseString;
                var tempToken = JsonConvert.DeserializeObject<Token>(responseString);
                if (tempToken.tokenHash != null && tempToken.tokenHash.Length == 40)
                {
                    ControlGroup.userToken = tempToken;
                    statusLabel.Text += "Token successfully added";
                }


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
            if (ControlGroup.userToken == null) return;

            try
            {
                LogOutQuery query = new LogOutQuery
                {
                    userToken = ControlGroup.userToken,
                };

                string urlPath = "http://localhost:60749/user/logOut";
                var request = (HttpWebRequest)WebRequest.Create(urlPath);
                request.Accept = "application/json";
                request.ContentType = "application/json";

                string requestBody = JsonConvert.SerializeObject(query);

                var data = Encoding.ASCII.GetBytes(requestBody);
                request.Method = "POST";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();

                ControlGroup.userToken = null;
                statusLabel.Text = ""+ responseString;
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }
    }

}
