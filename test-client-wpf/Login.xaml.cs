﻿using Newtonsoft.Json;
using SocialNetwork.Model;
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

            string urlPath = "http://localhost:51980/user/authenticate";
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
                return;

            var userToken = JsonConvert.DeserializeObject<Token>(responseString);
            statusLabel.Text = userToken.tokenHash + "\n" + userToken.userId;
            }catch(Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }

        private void loadLogin(object sender, RoutedEventArgs e)
        {
            //probably unnecessary
        }
    }

    class AuthenticateQuery
    {
        public string password { get; set; }
        public string username { get; set; }
    }
}