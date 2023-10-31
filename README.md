# EasyRent
1.link_to_sql
2. entity_framework

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
