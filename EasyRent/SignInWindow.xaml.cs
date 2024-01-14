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
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;


namespace EasyRent
{
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }
        BitmapImage bitmapImage;
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagini|*.png;*.jpg;*.jpeg;*.gif|Toate fisierele|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri imagePath = new Uri(openFileDialog.FileName);
                bitmapImage = new BitmapImage(imagePath);
                image.Source = bitmapImage;
            }

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            EasyRentDBDataContext dBDataContext = new EasyRentDBDataContext();
            byte[] photoData = ConvertBitmapImageToByteArray(bitmapImage);
            var existingUser = dBDataContext.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
            if (existingUser != null)
            {
                string message = "This username already exists.";
                Warning customDialog = new Warning(message);
                customDialog.Owner = this;
                customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                customDialog.ShowDialog();
            }
            else
            {
                if (txtPhoneNumber.Text.Length != 10)
                {
                    string message = "Phone number must be 10 digits long.";
                    Warning customDialog = new Warning(message);
                    customDialog.Owner = this;
                    customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    customDialog.ShowDialog();
                }
                else
                {

                    var existingEmail = dBDataContext.Users.FirstOrDefault(u => u.Email == txtEmail.Text);
                    if (existingEmail != null)
                    {
                        string message = "This email is already in use.";
                        Warning customDialog = new Warning(message);
                        customDialog.Owner = this;
                        customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        customDialog.ShowDialog();
                    }
                    else
                    {
                        if (cmbRole.SelectedItem == null)
                        {
                            string message = "Select your role";
                            Warning customDialog = new Warning(message);
                            customDialog.Owner = this;
                            customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            customDialog.ShowDialog();
                        }
                        else
                        {
                            var userE = new User
                            {
                                Name = txtName.Text,
                                Username = txtUsername.Text,
                                Password = HashPassword(pwdPassword.Password),
                                Email = txtEmail.Text,
                                PhoneNumber = txtPhoneNumber.Text,
                                Role = (cmbRole.SelectedItem as ComboBoxItem).Content.ToString(),
                                Photo = photoData
                            };
                            dBDataContext.Users.InsertOnSubmit(userE);
                            dBDataContext.SubmitChanges();
                            this.Close();
                        }
                    }
                }
            }
        }

        private byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
            {
                return null;
            }
            byte[] data;
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                data = stream.ToArray();
            }

            return data;
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


