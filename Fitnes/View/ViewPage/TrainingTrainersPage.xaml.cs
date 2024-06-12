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
    /// Логика взаимодействия для TrainingTrainersPage.xaml
    /// </summary>
    public partial class TrainingTrainersPage : Page
    {
        Core db = new Core();
        public TrainingTrainersPage()
        {
            InitializeComponent();

            var result = (from c in db.context.Client
              join u in db.context.Users on c.UserId equals u.IdUsers
              join target in db.context.Target on c.Target equals target.IdTarget
              join training in db.context.Training on c.IdClient equals training.ClientId
              group training by new { c.IdClient, u.Surname, target.TargetName } into trainingGroup
              select new
              {
                  IdClient = trainingGroup.Key.IdClient,
                  Surname = trainingGroup.Key.Surname,
                  Target = trainingGroup.Key.TargetName,
                  TotalTrainings = trainingGroup.Count()
                          }).ToList();
            StudentsGrid.ItemsSource = result;
            StudentsGrid.SelectedValuePath = "IdClient";
        }

        private void StudentsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             var activeTraining = Convert.ToInt32(StudentsGrid.SelectedValue);
          
            StudentsAllStackPanel.Visibility = Visibility.Hidden;
            TrainingAllStackPanel.Visibility = Visibility.Visible;
            var result = ( from  ex in db.context.Exercises
                           join t in db.context.Training on ex.IdExercises equals t.ExercisesId
                           join type in db.context.TypeExercises on ex.TypeExercisesId equals type.IdTypeExercises
                            
             where t.ClientId == activeTraining
                           select new
              {
                  IdExercises= ex.IdExercises,
                  TypeExercisesName =type.Name,
                  NameExercises = ex.NameExercises,
                  RegularityExercises = ex.RegularityExercises,
                  Progress = t.Progress
              }).ToList();
            TrainingGrid.ItemsSource = result;
            TrainingGrid.SelectedValuePath = "IdExercises";
             var resultAlltraining =(from ex in db.context.Exercises
                                     join type in db.context.TypeExercises on ex.TypeExercisesId equals type.IdTypeExercises
                                     select new
                                     {
                                         IdExercises = ex.IdExercises,
                                         TypeExercisesName = type.Name,
                                         NameExercises = ex.NameExercises,
                                         Duration = ex.Duration,
                                         Replays = ex.Replays,
                                         Quantity= ex.Quantity,
                                         RegularityExercises = ex.RegularityExercises,
                                     }).ToList();
            AllTrainingListView.ItemsSource = resultAlltraining;
            AllTrainingListView.SelectedValuePath = "IdExercises";
        }

        private void TrainingGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DelTraninButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddTraninButton_Click(object sender, RoutedEventArgs e)
        {
            var activeTraining = Convert.ToInt32(StudentsGrid.SelectedValue);
            //try

            //{
                Training training= new Training
                {
                    Progress = 0,
                ClientId = activeTraining,
                ExercisesId = Convert.ToInt32(AllTrainingListView.SelectedValue),
                TrenerId = App.CurrentTrainer.IdTrainer
            };
            Console.WriteLine(training);
            db.context.Training.Add(training);
                db.context.SaveChanges();

                MessageBox.Show("Принятие заявки выполнено успешно !",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            //}
            //catch
            //{
            //    MessageBox.Show("Критический сбор в работе приложения:", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void AllTrainingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
