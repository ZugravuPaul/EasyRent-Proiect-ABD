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
    public partial class SellHomeWindow : Window
    {
        public SellHomeWindow()
        {
            InitializeComponent();
            LoadAgents();
        }
        BitmapImage bitmapImage;
        private bool AreAllPropertyFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(nameTxt.Text))
                return false;

            if (string.IsNullOrWhiteSpace(addressTxt.Text))
                return false;

            if (!int.TryParse(zipcodeTxt.Text, out _))
                return false;

            if (string.IsNullOrWhiteSpace(citytxt.Text))
                return false;

            if (string.IsNullOrWhiteSpace(descriptionTxt.Text))
                return false;

            if (!decimal.TryParse(priceTxt.Text, out _))
                return false;

            if (!int.TryParse(roomsTxt.Text, out _))
                return false;

            if (!decimal.TryParse(areaTxt.Text, out _))
                return false;

            if (string.IsNullOrWhiteSpace(typeTxt.Text))
                return false;

            if (string.IsNullOrWhiteSpace(saletypeTxt.Text))
                return false;

            return true;
        }

        private void LoadAgents()
        {
            using (var context = new EasyRentDBDataContext())
            {
                var agents = context.Users.Where(u => u.Role == "Agent").ToList();
                var defaultOption = new User { Name = "No one" };
                agents.Insert(0, defaultOption);
                agentCmb.ItemsSource = agents;
                agentCmb.DisplayMemberPath = "Name";
            }
        }


        private void upBtn2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagini|*.png;*.jpg;*.jpeg;*.gif|Toate fisierele|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri imagePath = new Uri(openFileDialog.FileName);
                bitmapImage = new BitmapImage(imagePath);
                image2.Source = bitmapImage;
            }
        }

        private void upBtn1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagini|*.png;*.jpg;*.jpeg;*.gif|Toate fisierele|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri imagePath = new Uri(openFileDialog.FileName);
                bitmapImage = new BitmapImage(imagePath);
                image1.Source = bitmapImage;
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

        private void uploadBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new EasyRentDBDataContext())
            {
                
                if (AreAllPropertyFieldsFilled()==true)
                {
                    bool zipExists = false;
                    if (int.TryParse(zipcodeTxt.Text, out int zipCode))
                    {
                        zipExists = context.Properties.Any(p => p.ZipCode == zipCode);
                    }
                    if (zipExists)
                    {
                        string message = "A property with this ZipCode already exists";
                        Warning customDialog = new Warning(message);
                        customDialog.Owner = this;
                        customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        customDialog.ShowDialog();
                    }
                    int agentId = 0;
                    var selectedAgent = agentCmb.SelectedItem as User;
                    if (selectedAgent != null)
                    {
                        agentId = selectedAgent.UserId;
                    }
                    MainWindow mainWindow = (MainWindow)Owner;
                    int ownerId = 0;
                    if (!string.IsNullOrEmpty(MainWindow.UserName))
                    {
                        var ow = context.Users.FirstOrDefault(u => u.Username == MainWindow.UserName);
                        if (ow != null)
                        {
                            ownerId = ow.UserId;
                        }
                    }
                    var newProperty = new Property
                    {
                        Name = nameTxt.Text,
                        Address = addressTxt.Text,
                        ZipCode = int.Parse(zipcodeTxt.Text),
                        AgentID = agentId,
                        OwnerId = ownerId,
                        City = citytxt.Text,
                        Description = descriptionTxt.Text,
                        Price = decimal.Parse(priceTxt.Text),
                        Rooms = int.Parse(roomsTxt.Text),
                        Parking = parkingCheckBox.IsChecked ?? false ? 'Y' : 'N',
                        Area = decimal.Parse(areaTxt.Text),
                        Type = typeTxt.Text,
                        SaleType = saletypeTxt.Text,
                        Availability = 'Y',
                        PostTime = DateTime.Now
                    };
                    context.Properties.InsertOnSubmit(newProperty);

                    byte[] image1Data = ConvertBitmapImageToByteArray(image1.Source as BitmapImage);
                    byte[] image2Data = ConvertBitmapImageToByteArray(image2.Source as BitmapImage);
                    var image1Record = new Image
                    {
                        PropertyID = int.Parse(zipcodeTxt.Text),
                        Path = image1Data
                    };

                    var image2Record = new Image
                    {
                        PropertyID = int.Parse(zipcodeTxt.Text),
                        Path = image2Data
                    };
                    context.Images.InsertOnSubmit(image1Record);
                    context.Images.InsertOnSubmit(image2Record);
                    context.SubmitChanges();
                    this.Close();
                }
                else
                {
                    string message = "Empty fields!";
                    Warning customDialog = new Warning(message);
                    customDialog.Owner = this;
                    customDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    customDialog.ShowDialog();
                }
            }
        }
    }
}
