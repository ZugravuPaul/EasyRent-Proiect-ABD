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

namespace Interface
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

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signIn = new SignInWindow();
            signIn.Owner = this;
            signIn.ShowDialog();
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Enter an address, city or ZIP code...")
            {
                textBox.Text = "";
                textBox.FontStyle = FontStyles.Normal;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {

            LogInWindow logIn = new LogInWindow();
            logIn.Owner = this;
            logIn.ShowDialog();
        }
    }
}
