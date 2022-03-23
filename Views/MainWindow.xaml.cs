using System.Windows;
using System.Text.Json;
using RegistrationClient.Models;
using RegistrationClient.ViewModels;

namespace RegistrationClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ServiceViewModel service = new ServiceViewModel();

            if (!this.tbLogin.Text.Contains("@gmail.com"))
            {
                MessageBox.Show("Неверные данные! Почта должна вмещать в себе следующие символы: @gmail.com");
            }
            else if (this.tbPassword.Password.Length < 6)
            {
                MessageBox.Show("Неверные данные! Пароль слишком мал");
            }
            else
            {
                string data = JsonSerializer.Serialize(new User(this.tbLogin.Text, service.HashValue(this.tbPassword.Password)));
                MessageBox.Show(data);
                service.SendData(data);
            }
        }
    }
}
