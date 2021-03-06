﻿using Newtonsoft.Json;
using SocialNetwork2.Model;
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
using TestClientSN.Model;
using testClientWPF;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void submitRegistration(object sender, RoutedEventArgs e)
        {
            string username = this.username.Text;
            string password = this.password.Text;
            string name = this.name.Text;
            string lastName = this.surname.Text;
            string country = this.country.Text;
            string city = this.city.Text;
            string pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png";
            string coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png";
            string gender = "male";
            DateTime dateOfBirth = DateTime.Now;

            try
            {
                var request = new RegisterUserRequest
                {
                    username = username,
                    password = password,
                    name = name,
                    lastName = lastName,
                    country = country,
                    city = city,
                    pictureURL = pictureURL,
                    coverPictureURL = coverPictureURL,
                    gender = gender,
                    dateOfBirth = dateOfBirth,

                };

                this.username.Text = new ServiceConnector().registerUser(request);



                //statusLabel.Text = ControlGroup.userToken.tokenHash + "\n" + ControlGroup.userToken.userId;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.StackTrace;
            }
        }

        private void loadUserInfo(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;


            try
            {
                ConfidentialRequest request = new ConfidentialRequest()
                {
                    userToken = ClientInfo.Instance.SessionToken,
                    
                };

                var profileInfo = new ServiceConnector().loadUserInfo(request);

                //newsfeedContent.Text += responseString;
               

                if (profileInfo != null)
                {
                    name.Text = profileInfo.name;
                    surname.Text = profileInfo.lastName;
                    username.Text = profileInfo.username;
                    return;
                }
                else
                {
                    throw new Exception("Profile not exisisting");
                }

            }
            catch (Exception ex)
            {
                name.Text = ex.Message;
                return;
            }
        }

        private void editUserInfo(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;

            try
            {
                EditUserInfoRequest query = new EditUserInfoRequest()
                {
                    username = this.username.Text,
                    name = this.name.Text,
                    lastName = this.surname.Text,
                    country = this.country.Text,
                    city = this.city.Text,
                    pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png",
                    coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png",
                    gender = "male",
                    dateOfBirth = DateTime.Now,
                    userToken = ClientInfo.Instance.SessionToken
                };

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNServiceRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/settings/edit_info")
                    .Build();

                var responseString = request.requestFromServer();

                //newsfeedContent.Text += responseString;
                if (responseString == null) throw new Exception("Edit went wrong!");

                name.Text = (responseString);
            }
            catch (Exception ex)
            {
                name.Text = ex.Message;
            }

        }
    }
}
   
