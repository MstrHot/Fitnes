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

namespace Fitnes.View
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

       

       

        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length > 0)
            {
                Watemark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watemark.Visibility = Visibility.Visible;
            }
            if (PasswordRepeat.Password.Length > 0)
            {
                WatemarkRepeat.Visibility = Visibility.Collapsed;
            }
            else
            {
                WatemarkRepeat.Visibility = Visibility.Visible;
            }
        }

        private void ButtonEntrance_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрываем нынешнее окно

            // Создаем объект на основе определенного окан
            AuthsWindow AuthsWindow = new AuthsWindow();
            // Показываем новое окно
            AuthsWindow.Show();
            this.Close(); // Скрываем нынешнее окно
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {

        }

      
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
