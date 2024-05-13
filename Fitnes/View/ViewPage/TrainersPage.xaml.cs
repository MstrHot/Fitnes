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
    /// Логика взаимодействия для TrainersPage.xaml
    /// </summary>
    public partial class TrainersPage : Page
    {
        Core db = new Core();
        List <Trainer>  ArrayTrainer;
        List<Users> ArrayUsers;
        List<Achiev> ArrayAchiev;
        List<Achiev> Achiev;
        string FIO;
        //List <Users> фrrayUsers;
        public TrainersPage()
        {

            InitializeComponent();
            ArrayTrainer = db.context.Trainer.ToList() ;
            ArrayUsers =db.context.Users.ToList() ;
            TrainerComboBox.ItemsSource = ArrayTrainer ;
            TrainerComboBox.DisplayMemberPath = "IdTrainer";
            TrainerComboBox.SelectedValuePath = "IdTrainer";
            
            






        }

        private void ApplicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TrainerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Trainer Trainers = db.context.Trainer.Where(
                x => x.IdTrainer == TrainerComboBox.SelectedIndex+1).FirstOrDefault();
            Users UserTrainer = db.context.Users.Where(
                x => x.IdUsers == Trainers.UserId).FirstOrDefault();
            App.CurrentTrainerId = UserTrainer;
            FIO = ArrayUsers.FirstOrDefault(x => x.IdUsers== UserTrainer.IdUsers).FIO.ToString();
            NameTrainer.Text = FIO;
            AchievementsListView.Visibility=Visibility.Visible;

            ArrayAchiev = db.context.Achiev.ToList();
            
            AchievementsListView.ItemsSource = ArrayAchiev;
      

        }
    }
}
