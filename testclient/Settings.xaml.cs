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

namespace NeatClient
{
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void enableNameEdit(object sender, RoutedEventArgs e)
        {
            name.IsEnabled = true;
        }
        private void enableLastNameEdit(object sender, RoutedEventArgs e)
        {
            lastName.IsEnabled = true;
        }
        private void enableUsernameEdit(object sender, RoutedEventArgs e)
        {
            username.IsEnabled = true;
        }
        private void enablePasswordEdit(object sender, RoutedEventArgs e)
        {
            password.IsEnabled = true;
            reenteredPassword.IsEnabled = true;
        }
        private void enableCountryEdit(object sender, RoutedEventArgs e)
        {
            country.IsEnabled = true;
        }
        private void enableCityEdit(object sender, RoutedEventArgs e)
        {
            city.IsEnabled = true;
        }
        private void enableGenderEdit(object sender, RoutedEventArgs e)
        {
            femaleRadioButton.IsEnabled = true;
            maleRadioButton.IsEnabled = true;
        }
        private void enableDateEdit(object sender, RoutedEventArgs e)
        {
            date.IsEnabled = true;
        }

        private void submitChanges(object sender, RoutedEventArgs e)
        {
            var selectedDate = date.SelectedDate.Value.Date.ToShortDateString();
        }
    }
}