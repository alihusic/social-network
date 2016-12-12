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

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/register")
                    .Build();

                var responseString = request.requestFromServer();
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
