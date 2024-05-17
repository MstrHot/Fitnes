using Fitnes.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для TrainersPage.xaml
    /// </summary>
    public partial class TrainersPage : Page
    {
        Core db = new Core();
        List <Trainer>  arrayTrainer;
        List<Users> arrayUsers;
        List<Achiev> arrayAchiev;
        List<Request> arrayRequest;


        //List <Users> фrrayUsers;
        public TrainersPage()
        {

            InitializeComponent();
            arrayTrainer = db.context.Trainer.ToList() ;
            arrayUsers =db.context.Users.ToList() ;
           

            var result = from u in db.context.Users
                         join t in db.context.Trainer on u.IdUsers equals t.UserId
                         where u.UserType == 2
                         select new { User = u, Trainer = t };
          
         
            TrainerComboBox.ItemsSource = result.ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.User.FIO);
            }
            TrainerComboBox.DisplayMemberPath = "User.FIO";
            TrainerComboBox.SelectedValuePath = "Trainer.IdTrainer";


            arrayRequest = db.context.Request.Where(x => x.ClientId == App.CurrentUser.IdUsers).ToList();
            OrderListView.ItemsSource = arrayRequest;
           


        }

        private void ApplicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TrainerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            int activeTrainer = Convert.ToInt32(TrainerComboBox.SelectedValue);
            
           

            AchievementsListView.Visibility=Visibility.Visible;

            arrayAchiev = db.context.Achiev.Where(x => x.TrainerId== activeTrainer).ToList(); 

            AchievementsListView.ItemsSource = arrayAchiev;
           

        }
    }
}
