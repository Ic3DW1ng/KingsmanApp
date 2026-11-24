using System.Windows;

namespace KingsmenApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            new ServicesWindow { Owner = this }.ShowDialog();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Раздел «Сотрудники» можно сделать по образцу «Услуги».", "Информация");
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Раздел «Клиенты» можно сделать по образцу «Услуги».", "Информация");
        }
    }
}
