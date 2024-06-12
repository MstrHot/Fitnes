using Fitnes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Common;
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
    /// Логика взаимодействия для TrainingPage.xaml
    /// </summary>
    public partial class TrainingPage : Page
    {
        int activeTraining;
        Core db = new Core();
        ObservableCollection<View_TrainingDetails> collectionrainingDetails;
      
        public TrainingPage()
        {
           
            InitializeComponent();
            TrainingGrid.ItemsSource = db.context.ViewTraining.Where(x=> x.ClientId== App.CurrentClient.IdClient).ToList();
            TrainingGrid.SelectedValuePath = "IdTraining";

        }
      
      
      
        

        private void TrainingGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrainingAllStackPanel.Visibility = Visibility.Hidden;
            TrainingDetailsStackPanel.Visibility = Visibility.Visible;
            activeTraining = Convert.ToInt32(TrainingGrid.SelectedValue);
            
          
            TrainingDetailsGrid.ItemsSource = db.context.View_TrainingDetails.Where(x=>x.IdTraining == activeTraining).ToList();
            App.CurrentTrainingDetails = db.context.View_TrainingDetails.FirstOrDefault(x => x.IdTraining == activeTraining);



        }

      

      

        private void SaveTrainingButtun_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            Console.WriteLine();
            //App.CurrentTrainingDetails = (View_TrainingDetails)TrainingDetailsGrid.Items.CurrentItem;
            //Console.WriteLine(App.CurrentTrainingDetails);
            Training training = new Training()
            {
                IdTraining = Convert.ToInt32(TrainingGrid.SelectedValue),
                DoneExercises = App.CurrentTrainingDetails.DoneExercises,
                ClientId = App.CurrentClient.IdClient,
                ExercisesId = App.CurrentTrainingDetails.ExercisesId,
                TrenerId = App.CurrentTrainingDetails.TrenerId,
                Progress = App.CurrentTrainingDetails.Progress
            };


           



            db.context.Training.AddOrUpdate(training);
            db.context.SaveChanges();
            MessageBox.Show("Сохрание успешно",
                 "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            //}
            //catch
            //{
            //    MessageBox.Show("Критический сбор в работе приложения:", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }
    }
}
