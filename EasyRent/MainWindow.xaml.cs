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
        public static string UserName = null;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = 100; 
            Top = 50;  
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
            profile.ShowDialog();
            //this.Close();

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SellHomeWindow sellHome = new SellHomeWindow();
            sellHome.ShowDialog();
        }
    }




}
