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

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openLogin(object sender, RoutedEventArgs e)
        {
            Main.Content = new Login();
        }

        private void openSignup(object sender, RoutedEventArgs e)
        {
            Main.Content = new Signup();
        }
        private void openChat(object sender, RoutedEventArgs e)
        {
            Main.Content = new Chat();
        }

        private void openCheckNewMessages(object sender, RoutedEventArgs e)
        {
            Main.Content = new CheckNewMessages();
        }

        private void openNewsfeed(object sender, RoutedEventArgs e)
        {
            Main.Content = new Newsfeed();
        }

        private void openViewPost(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewPost();
        }

        private void openCreatePost(object sender, RoutedEventArgs e)
        {
            Main.Content = new CreatePost();
        }
    }
}
