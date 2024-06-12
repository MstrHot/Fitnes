using Fitnes.Model;
using Microsoft.Build.BuildEngine;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Fitnes.View.ViewPage
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        Core db = new Core();
        List<Pol> arrayPol;
        List<Model.Target> arrayTarget;
        public ProfilePage()
        {
            InitializeComponent();
            UserName.Text = App.CurrentUser.Name;
            Surname.Text = App.CurrentUser.Surname;
            Patronymic.Text = App.CurrentUser.Patronymic;
            Telephone.Text = App.CurrentUser.Nunber;
            Password.Text = App.CurrentUser.Password;
            Mail.Text = App.CurrentUser.Email;
            arrayPol = db.context.Pol.ToList();
            PolComboBox.ItemsSource = arrayPol;
            PolComboBox.DisplayMemberPath = "NamePol";
            PolComboBox.SelectedValuePath = "IdPol";
            
            arrayTarget = db.context.Target.ToList();
            TargetComboBox.ItemsSource = arrayTarget;
            TargetComboBox.DisplayMemberPath = "TargetName";
            TargetComboBox.SelectedValuePath = "IdTarget";
            WeightTextBox.Text = Convert.ToString(App.CurrentUser.Weight);

            if(App.CurrentUser.Pol1!= null)
                PolComboBox.Text = App.CurrentUser.Pol1.NamePol;
            if (App.CurrentUser.UserType == 3)   
            {
                if (App.CurrentClient.Target != null)
                    TargetComboBox.Text = App.CurrentClient.Target1.TargetName;

            }
           
              
            
           
        }

        private void Pol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            //arrayPol = db.context.Pol.ToList();
            //PolComboBox.ItemsSource = arrayPol;
            //PolComboBox.DisplayMemberPath = "TitleGroup";
            //    PolComboBox.SelectedValuePath = "IdGroup";
            
           
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try

            {


                App.CurrentUser.Name = UserName.Text;
                App.CurrentUser.Surname = Surname.Text;
                App.CurrentUser.Patronymic = Patronymic.Text;
                App.CurrentUser.Email = Mail.Text;
                App.CurrentUser.Password = Password.Text;
                App.CurrentUser.Pol = Convert.ToInt32(PolComboBox.SelectedValue);
                App.CurrentUser.Nunber = Telephone.Text;
                App.CurrentUser.Weight = Convert.ToInt32(WeightTextBox.Text);
                App.CurrentClient.Target = Convert.ToInt32(TargetComboBox.SelectedValue);


                db.context.Users.AddOrUpdate(App.CurrentUser);
                db.context.Client.AddOrUpdate(App.CurrentClient);
                db.context.SaveChanges();

                MessageBox.Show("Добавление выполнено успешно !",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            }
            catch
            {
                MessageBox.Show("Критический сбор в работе приложения:", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TargetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
