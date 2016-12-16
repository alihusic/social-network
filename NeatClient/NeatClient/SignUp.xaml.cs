using Newtonsoft.Json;
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
using System.Windows.Shapes;
using testClientWPF;

namespace NeatClient
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void signUpButtonClick(object sender, RoutedEventArgs e)
        {
            string username = this.usernameInput.Text;
            string password = this.passwordInput.Text;
            string name = this.nameInput.Text;
            string lastName = this.surnameInput.Text;
            string country = this.countryInput.Text;
            string city = this.cityInput.Text;
            string pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png";
            string coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png";
            string gender = femaleButton.IsChecked==true ? "female":"male";
            DateTime dateOfBirth = DateTime.Now;

            try
            {
                var query = new RegisterUserRequest
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

                string requestBody = JsonConvert.SerializeObject(query);

                var request = new SNRequestBuilder()
                    .Accept("application/json")
                    .ContentType("application/json")
                    .RequestBody(requestBody)
                    .RequestMethod("POST")
                    .UrlSubPath("/user/register")
                    .Build();

                var responseString = request.requestFromServer();
                MessageBox.Show(responseString);


                //statusLabel.Text = ControlGroup.userToken.tokenHash + "\n" + ControlGroup.userToken.userId;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.StackTrace;
                MessageBox.Show(ex.Message);
            }
            //checks that client makes go here !
            /*MainWindow neat = new MainWindow();
            neat.Show();
            this.Close();*/
        }

        private void logInButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow logIn = new MainWindow();
            logIn.Show();
            this.Close();
        }
    }
}
