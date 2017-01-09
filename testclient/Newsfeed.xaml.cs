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
using TestClientSN.Model;

namespace NeatClient
{
    /// <summary>
    /// Interaction logic for Newsfeed.xaml
    /// </summary>
    public partial class Newsfeed : Page
    {
        public Newsfeed()
        {
            InitializeComponent();

            List<ProfileInfo> list = new List<ProfileInfo>();
            for (int i = 0; i < 9; i++)
            {
                list.Add(new ProfileInfo(i));
            }
            DataContext = list;
        }

        private void openCommentSection(object sender, RoutedEventArgs e)
        {
            foreach (WrapPanel commentSection in FindVisualChildren<WrapPanel>(postPresenter))
            {
                commentSection.Visibility = Visibility.Visible;
            }
        }
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}
