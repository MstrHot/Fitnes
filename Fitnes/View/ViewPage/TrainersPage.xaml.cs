using Fitnes.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        List<Achiev> arrayAchiev;
        List<Request> arrayRequests;
       


        //List <Users> фrrayUsers;
        public TrainersPage()
        {

            InitializeComponent();

           
           

            var result = from u in db.context.Users
                         join t in db.context.Trainer on u.IdUsers equals t.UserId
                         where u.UserType == 2
                         select new { User = u, Trainer = t };

       

            TrainerComboBox.ItemsSource = result.ToList();

            TrainerComboBox.DisplayMemberPath = "User.FIO";
            TrainerComboBox.SelectedValuePath = "Trainer.IdTrainer";

            var resultOrder = from r in db.context.Request
                              join s in db.context.Stuts on r.StatusId equals s.IdStuts
                              select new { IdRequest = r, Type = s };

            OrderListView.ItemsSource = db.context.View_RequstStuts.Where(x=> x.ClientId == App.CurrentClient.IdClient).ToList();


            

         

        }

        private void ApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            
            arrayRequests = db.context.Request.Where(x=> x.ClientId== App.CurrentClient.IdClient).ToList();
            if (arrayRequests.Where(x=> x.TrenerId == Convert.ToInt32(TrainerComboBox.SelectedValue)).Count() < 1 )
            {

           
            Request newRequest = new Request()

            {
                StatusId=3,
                ClientId=App.CurrentClient.IdClient,
                TrenerId= Convert.ToInt32(TrainerComboBox.SelectedValue)
               

            };



            db.context.Request.AddOrUpdate(newRequest);
            db.context.SaveChanges();


            MessageBox.Show("Заявка выполнено успешно !",
            "Уведомление",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Вторую заявку нельзя отправить одному трениру");
                MessageBox.Show("Повторно можео будет отправить заявку тренеру после отказа спустя 7 дней");
            }
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
