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

namespace EasyRentWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
                
        }
        EasyRentDBDataContext db=new EasyRentDBDataContext(Properties.Settings.Default.EasyRentDataBaseConnectionString);

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Username = txtUsername.Text;
            user.Password = passPassword.Password;
            user.Email = txtEmail.Text;
            user.PhoneNumber = txtPhone.Text;
            user.Role = "ADMIN";

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                MessageBox.Show("Campuri necompletate");
                return;
            }

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();
            MessageBox.Show("Bravo tati...");
        }
    }
}
