using System.Windows;
using KingsmenApp.Services;

namespace KingsmenApp
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (login.Length == 0 || password.Length == 0)
            {
                ErrorText.Text = "Введите логин и пароль";
                return;
            }

            var user = DataStore.FindUser(login);
            if (user == null || user.PasswordHash != PasswordHelper.Hash(password))
            {
                ErrorText.Text = "Неверный логин или пароль";
                return;
            }

            new MainWindow().Show();
            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow { Owner = this }.ShowDialog();
        }
    }
}
