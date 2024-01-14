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


namespace EasyRent
{

    public partial class MainWindow : Window
    {
        private Profile profile;
        private SellHomeWindow sell;
        public static string UserName = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Owner = this;
            logInWindow.ShowDialog();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Owner = this;
            signInWindow.ShowDialog();
        }

        private void btnShowProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Owner = this;
            profile.ShowDialog();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                string message = "You need to log in first.";
                Warning customDialog = new Warning(message);
                customDialog.Owner = this;
                customDialog.ShowDialog();
            }
            else
            {
                SellHomeWindow sellHome = new SellHomeWindow();
                sellHome.Owner = this;
                sellHome.ShowDialog();
            }
        }

        private void Image_MouseDownBuy(object sender, MouseButtonEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(UserName))
            {
                string message = "You need to log in first.";
                Warning customDialog = new Warning(message);
                customDialog.Owner = this;
                customDialog.ShowDialog();
            }
            else
            {
                Houses houses = new Houses();
                houses.Owner = this;
                houses.ShowDialog();
            }
        }
    }




}
