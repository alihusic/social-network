using SocialNetwork.Model;
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
using TestClientSN.Model;

namespace NeatClient
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Container : Window
    {
        public Container()
        {
            InitializeComponent();
            pageContainer.Content = new Newsfeed(); 
            List<ProfileInfo> list = new List<ProfileInfo>();
            for(int i = 0; i < 9; i++)
            {
                list.Add(new ProfileInfo(i));
            }
            DataContext = list;
        }
        

        private void openSettings(object sender, RoutedEventArgs e)
        {
            pageContainer.Content = new Settings();
        }

        private void openNewsfeed(object sender, RoutedEventArgs e)
        {
            pageContainer.Content = new Newsfeed();
        }

        private void openProfile(object sender, RoutedEventArgs e)
        {
            pageContainer.Content = new Profile();
        }
    }
}
