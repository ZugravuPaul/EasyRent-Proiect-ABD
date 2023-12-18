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
//using System.Windows.Media.Imaging;



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
            var userE = new User
            {
                Name = txtName.Text.ToString(),
                Username = txtUsername.Text.ToString(),
                Password = txtPassword.Text.ToString(),
                Email = txtEmail.Text.ToString(),
                PhoneNumber = txtPhoneNumber.Text.ToString(),
                Role = txtRole.Text.ToString(),
                Photo=photoData
                
        };
            dBDataContext.Users.InsertOnSubmit(userE);
            dBDataContext.SubmitChanges();
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




    }
}
