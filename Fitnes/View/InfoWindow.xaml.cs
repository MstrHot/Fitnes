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
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PersonalArea_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрываем нынешнее окно

            // Создаем объект на основе определенного окан
            MainWindow MainWindow = new MainWindow();
            // Показываем новое окно
            MainWindow.Show();
            this.Close(); // Скрываем нынешнее окно
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void BuySubscription_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
