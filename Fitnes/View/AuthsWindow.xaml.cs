using Fitnes.Model;
using Fitnes.ViewModel;
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
    /// Логика взаимодействия для AuthsWindow.xaml
    /// </summary>
    public partial class AuthsWindow : Window
    {
        Core db = new Core();
        public AuthsWindow()
        {
            InitializeComponent();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрываем нынешнее окно

            // Создаем объект на основе определенного окан
            RegWindow RegWindow = new RegWindow();
            // Показываем новое окно
            RegWindow.Show();
            this.Close(); // Скрываем нынешнее окно
        }

        private void ButtonEntrance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserVM newObject = new UserVM();
                bool result = newObject.CheckAuth(Telephone.Text, password.Password);
                if (result)
                {
                    Users item = db.context.Users.Where(
                        x => x.Nunber == Telephone.Text &&
                        x.Password == password.Password)
                        .FirstOrDefault();
                    App.CurrentUser = item;
                    App.CurrentClient = db.context.Client.FirstOrDefault(x=>x.UserId== item.IdUsers);

                    Users Type = db.context.Users.Where(
                        x => x.UserType == 2).FirstOrDefault();
                    App.CurrentUserType = Type;
                    App.CurrentTrainer = db.context.Trainer.FirstOrDefault();
                    

                    this.Hide(); // Скрываем нынешнее окно

                    // Создаем объект на основе определенного окан
                    InfoWindow InfoWindow = new InfoWindow();
                    // Показываем новое окно
                    InfoWindow.Show();
                    this.Close(); // Скрываем нынешнее окно

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password.Length > 0)
            {
                watemark.Visibility = Visibility.Collapsed;
            }
            else
            {
                watemark.Visibility = Visibility.Visible;
            }
        }

       

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();

        }
    }
}
