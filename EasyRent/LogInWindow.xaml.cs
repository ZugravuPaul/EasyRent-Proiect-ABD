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

namespace EasyRent
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            EasyRentDBDataContext dBDataContext = new EasyRentDBDataContext();

            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Text; // Use Password property for password boxes

            var users = from user in dBDataContext.Users
                        select user;

            foreach (User user in users)
            {
                if (user.Password == enteredPassword && user.Username == enteredUsername)
                {
                    this.Close();
                    return; 
                }
            }
            MessageBox.Show("Invalid username or password. Please try again.");
        }


    }
}
