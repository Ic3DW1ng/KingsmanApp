using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using KingsmenApp.Models;
using KingsmenApp.Services;
using Microsoft.Win32;

namespace KingsmenApp
{
    public partial class EditServiceWindow : Window
    {
        private readonly Service _service;   
        private string _imagePath;

        public EditServiceWindow(Service service = null)
        {
            InitializeComponent();
            _service = service;
            TypeBox.ItemsSource = ServiceTypes.All;

            if (service != null)
            {
                TitleText.Text = "Услуга";
                NameBox.Text = service.Name;
                DescriptionBox.Text = service.Description;
                PriceBox.Text = service.Price.ToString(CultureInfo.CurrentCulture);
                TypeBox.SelectedItem = service.ServiceType;
                SetImage(service.ImagePath);
                SaveButton.Content = "Изменить";
            }
            else
            {
                SaveButton.Content = "Добавить";
            }
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Выберите изображение",
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (dialog.ShowDialog(this) == true)
                SetImage(dialog.FileName);
        }

        private void SetImage(string path)
        {
            _imagePath = path;
            if (string.IsNullOrEmpty(path))
            {
                ServiceImage.Source = null;
                return;
            }

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad; 
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            ServiceImage.Source = bitmap;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string description = DescriptionBox.Text.Trim();
            string type = TypeBox.SelectedItem as string;

            if (name.Length == 0)
            {
                Warn("Введите наименование услуги");
                return;
            }

            if (!decimal.TryParse(PriceBox.Text.Trim(), NumberStyles.Number,
                                  CultureInfo.CurrentCulture, out decimal price) || price <= 0)
            {
                Warn("Введите корректную цену");
                return;
            }

            if (type == null)
            {
                Warn("Выберите тип услуги");
                return;
            }

            if (_service == null)
            {
                DataStore.AddService(new Service
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    ServiceType = type,
                    ImagePath = _imagePath
                });
            }
            else
            {
                _service.Name = name;
                _service.Description = description;
                _service.Price = price;
                _service.ServiceType = type;
                _service.ImagePath = _imagePath;
            }

            Close();
        }

        private void Warn(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
