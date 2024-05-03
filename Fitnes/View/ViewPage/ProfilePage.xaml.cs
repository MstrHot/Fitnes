using Fitnes.Model;
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
        List<Targe>
        public ProfilePage()
        {
            InitializeComponent();
            UserName.Text = App.CurrentUser.Name;
            Surname.Text = App.CurrentUser.Surname;
            Patronymic.Text = App.CurrentUser.Patronymic; 
            Telephone.Text = App.CurrentUser.Nunber;
            Mail.Text = App.CurrentUser.Email;
            arrayPol = db.context.Pol.ToList();
            PolComboBox.ItemsSource = arrayPol;
            PolComboBox.DisplayMemberPath = "NamePol";
            PolComboBox.SelectedValuePath = "IdPol";
            PolComboBox.Text = App.CurrentUser.Pol1.NamePol;
            TargetComboBox.ItemsSource=
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
                Users users = new Users()
                {
                    
                    Name = UserName.Text,
                    Surname = Surname.Text
                    
};

                db.context.Users.Add(users);
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
