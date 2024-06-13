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
        int idActiveTraining;
        Core db = new Core();
        List<View_TrainingDetails> arrayDetail;
        ObservableCollection<string> collectionrainingDetails;
      
        public TrainingPage()
        {
           
            InitializeComponent();
            TrainingGrid.ItemsSource = db.context.ViewTraining.Where(x=> x.ClientId== App.CurrentClient.IdClient).ToList();
            TrainingGrid.SelectedValuePath = "IdTraining";

        }





        private void TrainingGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TrainingAllStackPanel.Visibility = Visibility.Hidden;
                TrainingDetailsStackPanel.Visibility = Visibility.Visible;
                idActiveTraining = Convert.ToInt32(TrainingGrid.SelectedValue);
                var resultOrder = (from t in db.context.Training

                                   join ex in db.context.Exercises on t.ExercisesId equals ex.IdExercises
                                   join typeE in db.context.TypeExercises on ex.TypeExercisesId equals typeE.IdTypeExercises
                                   where t.IdTraining == idActiveTraining
                                   select new
                                   {
                                       IdTraining = t.IdTraining,
                                       Name = typeE.Name,
                                       NameExercises = ex.NameExercises,
                                       Duration = ex.Duration,
                                       RegularityExercises = ex.RegularityExercises,
                                       Replays = ex.Replays,
                                       Quantity = ex.Quantity,
                                       Progress = t.Progress,
                                       ClientId = t.ClientId,
                                       DoneExercises = t.DoneExercises,
                                       ExercisesId = t.ExercisesId,
                                       TrenerId = t.TrenerId
                                   }).ToList();





                //Training activeTraining = TrainingGrid.SelectedValue as Training;
                //activeTraining.IdTraining=

                App.CurrentTrainingDetails = db.context.View_TrainingDetails.FirstOrDefault(x => x.IdTraining == idActiveTraining);
                //arrayDetail = resultOrder.Where(x=> x.IdTraining,);
                TrainingDetailsGrid.ItemsSource = resultOrder;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);




            }
        }





        private void SaveTrainingButtun_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            Console.WriteLine();
      
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


           



            db.context.Training.Add(training);
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
