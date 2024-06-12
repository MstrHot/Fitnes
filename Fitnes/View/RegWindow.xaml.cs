using Fitnes.Model;
using Fitnes.View.CheckClasses;
using Fitnes.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Core db = new Core();
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

            string password = Password.Password;
            string telephone;
            if (Regex.Match(Telephone.Text, "[+]").Success)
            {
                 telephone = Telephone.Text.Substring(1);
            }
            else
            {
                 telephone = Telephone.Text;
            }
           
            if (RegCheck.TelephoneCheck(telephone) ==2)
            {
                if (Name.Text.Length > 1 & Surname.Text.Length > 1 & Patronymic.Text.Length > 1)
                {

                    if (RegCheck.PasswordCheck(password) == 5)
                    {
                        if (Password.Password == PasswordRepeat.Password)
                        {


                            try

                            {
                                Users regUser = new Users()

                                {

                                    UserType = 3,
                                    Nunber = Telephone.Text,
                                    Password = Password.Password,
                                    Name = Name.Text,
                                    Surname = Surname.Text,
                                    Patronymic = Patronymic.Text

                                };



                                db.context.Users.Add(regUser);
                                db.context.SaveChanges();


                                MessageBox.Show("Регистрация выполнено успешно !",
                                "Уведомление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                                // Создаем объект на основе определенного окан
                                AuthsWindow AuthsWindow = new AuthsWindow();
                                // Показываем новое окно
                                AuthsWindow.Show();
                                this.Close(); // Скрываем нынешнее окно
                            }

                            catch
                            {
                                MessageBox.Show("Критический сбор в работе приложения:", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    
                    else
                    {
                        MessageBox.Show("Слишком легкий пароль. Используйте цифры, Латинские буквы, Спец. символы и длина проля должна состоять из 7 симвалов ", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля ", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Некореткный номер", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

      
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
