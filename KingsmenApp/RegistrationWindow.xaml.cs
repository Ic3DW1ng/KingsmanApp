using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using KingsmenApp.Models;
using KingsmenApp.Services;

namespace KingsmenApp
{
    public partial class RegistrationWindow : Window
    {
        private static readonly string[] Sizes = { "XS", "S", "M", "L", "XL", "XXL" };

        public RegistrationWindow()
        {
            InitializeComponent();
            GenderBox.ItemsSource = new[] { "Мужской", "Женский" };
            TopSizeBox.ItemsSource = Sizes;
            BottomSizeBox.ItemsSource = Sizes;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var errors = new List<string>();

            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;
            string phone = PhoneBox.Text.Trim();
            string email = EmailBox.Text.Trim();

            if (login.Length == 0)
                errors.Add("Введите логин");
            else if (DataStore.FindUser(login) != null)
                errors.Add("Пользователь с таким логином уже существует");

            if (password.Length < 4)
                errors.Add("Пароль должен содержать не менее 4 символов");

            if (LastNameBox.Text.Trim().Length == 0) errors.Add("Введите фамилию");
            if (FirstNameBox.Text.Trim().Length == 0) errors.Add("Введите имя");
            if (BirthDatePicker.SelectedDate == null) errors.Add("Выберите дату рождения");
            if (GenderBox.SelectedItem == null) errors.Add("Выберите пол");

            if (!Regex.IsMatch(phone, @"^\+?\d{10,15}$"))
                errors.Add("Некорректный телефон (10–15 цифр)");

            
            if (email.Length > 0 && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Некорректный e-mail");

            int? shoe = null;
            string shoeText = ShoeSizeBox.Text.Trim();
            if (shoeText.Length > 0)
            {
                if (int.TryParse(shoeText, out int s) && s >= 30 && s <= 50)
                    shoe = s;
                else
                    errors.Add("Размер обуви — число от 30 до 50");
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataStore.AddUser(new User
            {
                Login = login,
                PasswordHash = PasswordHelper.Hash(password),
                LastName = LastNameBox.Text.Trim(),
                FirstName = FirstNameBox.Text.Trim(),
                MiddleName = MiddleNameBox.Text.Trim(),
                BirthDate = BirthDatePicker.SelectedDate,
                Gender = GenderBox.SelectedItem as string,
                Phone = phone,
                Email = email,
                TopSize = TopSizeBox.SelectedItem as string,
                BottomSize = BottomSizeBox.SelectedItem as string,
                ShoeSize = shoe
            });

            MessageBox.Show("Регистрация прошла успешно!", "Готово",
                            MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
