<<<<<<< HEAD
﻿using Newtonsoft.Json;
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
            string pictureURL = "http://arhiva.alo.rs/resources/img/03-09-2012/home_category/9804-23.jpg";
            string coverPictureURL = "http://arhiva.alo.rs/resources/img/03-09-2012/home_category/9804-23.jpg";
            string gender = "male";
            DateTime dateOfBirth = DateTime.Now;

            try
            {
                var query = new RegisterQuery
                {
                    username = username,
                    password = password,
                    name=name,
                    lastName=lastName,
                    country=country,
                    city=city,
                    pictureURL=pictureURL,
                    coverPictureURL=coverPictureURL,
                    gender=gender,
                    dateOfBirth=dateOfBirth,
                    
                };

                string urlPath = "http://localhost:60749/user/register";
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
                this.username.Text = responseString;
                


                //statusLabel.Text = ControlGroup.userToken.tokenHash + "\n" + ControlGroup.userToken.userId;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.StackTrace;
            }
        }
    }
}
=======
﻿using Newtonsoft.Json;
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
            string pictureURL = "http://arhiva.alo.rs/resources/img/03-09-2012/home_category/9804-23.jpg";
            string coverPictureURL = "http://arhiva.alo.rs/resources/img/03-09-2012/home_category/9804-23.jpg";
            string gender = "male";
            DateTime dateOfBirth = DateTime.Now;

            try
            {
                var query = new RegisterQuery
                {
                    username = username,
                    password = password,
                    name=name,
                    lastName=lastName,
                    country=country,
                    city=city,
                    pictureURL=pictureURL,
                    coverPictureURL=coverPictureURL,
                    gender=gender,
                    dateOfBirth=dateOfBirth,
                    
                };

                string urlPath = "http://localhost:60749/user/register";
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
                this.username.Text = responseString;
                


                //statusLabel.Text = ControlGroup.userToken.tokenHash + "\n" + ControlGroup.userToken.userId;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.StackTrace;
            }
        }
    }
}
>>>>>>> refs/remotes/origin/Maulwurf
