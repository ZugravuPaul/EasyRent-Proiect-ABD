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
using System.IO;


namespace EasyRent
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
            setProfileInformation();
        }

        void setProfileInformation()
        {
            EasyRentDBDataContext dBDataContext = new EasyRentDBDataContext();
            var users = from user in dBDataContext.Users
                        select user;
            foreach (User user in users)
            {
                if (user.Username == MainWindow.UserName)
                {
                    if (user.Photo != null && user.Photo.Length > 2)
                    {
                        BitmapImage bitmapImage = ConvertByteArrayToBitmapImage(user.Photo.ToArray());
                        profileImage.Source = bitmapImage;
                    }

                    lblUsername.Content = "Username " + user.Username;
                    lblName.Content = "Name " + user.Name;
                    lblEmail.Content = "Email " + user.Email;
                    lblPhoneNumber.Content = "Contact " + user.PhoneNumber;
                    lblRole.Content = "Role " + user.Role;
                }
            }
        }
        private BitmapImage ConvertByteArrayToBitmapImage(byte[] imageData)
        {
            if (imageData == null)
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;
            if (mainWindow != null)
            {
                MainWindow.UserName = null;
                mainWindow.btnLogIn.Visibility = Visibility.Visible;
                mainWindow.btnSignIn.Visibility = Visibility.Visible;
                mainWindow.btnShowProfile.Visibility = Visibility.Collapsed;
            }
            this.Close();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
