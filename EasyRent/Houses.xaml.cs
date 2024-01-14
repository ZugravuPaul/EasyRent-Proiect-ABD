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
    /// Interaction logic for Houses.xaml
    /// </summary>
    public partial class Houses : Window
    {
        public Houses()
        {
            InitializeComponent();
            LoadProperties();
        }
        private void LoadProperties()
        {
            EasyRentDBDataContext dBDataContext = new EasyRentDBDataContext();
            var properties = (from property in dBDataContext.Properties
                              select property).Take(3).ToList();
            name1.Content = $"Name: {properties[0].Name}";
            add1.Content = $"Address: {properties[0].Address}";
            zc1.Content = $"Zip Code: {properties[0].ZipCode}";
            city1.Content = $"City: {properties[0].City}";
            type1.Content = $"Type: {properties[0].Type}";
            sale1.Content = $"For Sale: {properties[0].Type}";
            desc1.Content = $"Description: {properties[0].Description}";
            post1.Content = $"Post Date: {properties[0].PostTime}";
            area1.Content = $"Area: {properties[0].Area}";
            price1.Content = $"Price: {properties[0].Price}";
            rooms1.Content = $"Rooms: {properties[0].Rooms}";

            var owner = dBDataContext.Users.FirstOrDefault(user => user.UserId == properties[0].OwnerId);
            if (owner != null)
            {
                contact1.Content = $"Owner Phone:\n {owner.PhoneNumber}";
            }

            //var propertyImages = dBDataContext.Images
            //                               .Where(img => img.PropertyID == properties[0].PropertyID)
            //                               .ToList();
            //BitmapImage bitmapImage1 = ConvertByteArrayToBitmapImage(propertyImages[0].Path.ToArray());
            //image1.Source = bitmapImage1;

            //BitmapImage bitmapImage2 = ConvertByteArrayToBitmapImage(propertyImages[1].Path.ToArray());
            //image2.Source = bitmapImage2;

            name2.Content = $"Name: {properties[1].Name}";
            add2.Content = $"Address: {properties[1].Address}";
            zc2.Content = $"Zip Code: {properties[1].ZipCode}";
            city2.Content = $"City: {properties[1].City}";
            type2.Content = $"Type: {properties[1].Type}";
            sale2.Content = $"For Sale: {properties[1].Type}";
            desc2.Content = $"Description: {properties[1].Description}";
            post2.Content = $"Post Date: {properties[1].PostTime}";
            area2.Content = $"Area: {properties[1].Area}";
            price2.Content = $"Price: {properties[1].Price}";
            rooms2.Content = $"Rooms: {properties[1].Rooms}";

            owner = dBDataContext.Users.FirstOrDefault(user => user.UserId == properties[1].OwnerId);
            if (owner != null)
            {
                contact2.Content = $"Owner Phone:\n {owner.PhoneNumber}";
            }


            name3.Content = $"Name: {properties[2].Name}";
            add3.Content = $"Address: {properties[2].Address}";
            zc3.Content = $"Zip Code: {properties[2].ZipCode}";
            city3.Content = $"City: {properties[2].City}";
            type3.Content = $"Type: {properties[2].Type}";
            sale3.Content = $"For Sale: {properties[2].Type}";
            desc3.Content = $"Description: {properties[2].Description}";
            post3.Content = $"Post Date: {properties[2].PostTime}";
            area3.Content = $"Area: {properties[2].Area}";
            price3.Content = $"Price: {properties[2].Price}";
            rooms3.Content = $"Rooms: {properties[2].Rooms}";

            owner = dBDataContext.Users.FirstOrDefault(user => user.UserId == properties[2].OwnerId);
            if (owner != null)
            {
                contact3.Content = $"Owner Phone:\n {owner.PhoneNumber}";
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

    }
}
