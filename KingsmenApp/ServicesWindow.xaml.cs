using System.Windows;
using System.Windows.Input;
using KingsmenApp.Models;
using KingsmenApp.Services;

namespace KingsmenApp
{
    public partial class ServicesWindow : Window
    {
        public ServicesWindow()
        {
            InitializeComponent();
            ServicesGrid.ItemsSource = DataStore.Services;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new EditServiceWindow { Owner = this }.ShowDialog();
            ServicesGrid.Items.Refresh();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Service selected)
            {
                new EditServiceWindow(selected) { Owner = this }.ShowDialog();
                ServicesGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Выберите услугу в списке", "Внимание");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Service selected &&
                MessageBox.Show($"Удалить услугу «{selected.Name}»?", "Подтверждение",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataStore.DeleteService(selected);
            }
        }

        private void ServicesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit_Click(sender, e);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
