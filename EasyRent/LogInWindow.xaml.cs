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
using System.Security.Cryptography;

namespace EasyRent
{
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
            string enteredPassword = HashPassword(txtPassword.Password);
            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                string customMessage1 = "Please enter both username and password";
                Warning customDialog1 = new Warning(customMessage1);
                customDialog1.Owner = this;
                customDialog1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customDialog1.ShowDialog();
            }
            else
            {

                var users = from user in dBDataContext.Users
                            select user;
                foreach (User user in users)
                {
                    if (user.Password == enteredPassword && user.Username == enteredUsername)
                    {

                        MainWindow mainWindow = (MainWindow)Owner;
                        MainWindow.UserName = user.Username;
                        mainWindow.btnLogIn.Visibility = Visibility.Collapsed;
                        mainWindow.btnSignIn.Visibility = Visibility.Collapsed;
                        mainWindow.btnShowProfile.Visibility = Visibility.Visible;
                        mainWindow.btnShowProfile.Content = user.Username;
                        this.Close();
                        return;
                    }
                }
                string customMessage = "Invalid username or password. Please try again.";
                Warning customDialog = new Warning(customMessage);
                customDialog.Owner = this;
                customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customDialog.ShowDialog();
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
