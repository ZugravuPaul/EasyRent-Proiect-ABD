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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //try
            //{
            //    EasyRentDBDataContext DB = new EasyRentDBDataContext();
            //    var userE = new User
            //    {
            //        Username = "aaa",
            //        Password = "aaa",
            //        Email = "bbb",
            //        Photo = null,
            //        PhoneNumber = "0745897455",
            //        Role = "agent", 
            //        Name="aa"

            //    };
            //    DB.Users.InsertOnSubmit(userE);
            //    DB.SubmitChanges();
            //    var users = from user in DB.Users
            //                select user;
            //    foreach (User user in users)
            //    {
            //        Console.WriteLine(user.Email);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Eroare la inserare: {ex.Message}");
            //}
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
    }




}
