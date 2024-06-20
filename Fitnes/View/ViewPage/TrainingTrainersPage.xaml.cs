using Fitnes.Model;
using Fitnes.View.CheckClasses;
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
        List<TypeExercises> arrayTypeExercises;
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
            try
            {

                var item = TrainingGrid.SelectedItems as Training ;

                //проверка того, что пользователь выбрал строки для удаления

                if (item == null)
                {
                    MessageBox.Show("Вы не выбрали ни одной строки");
                    return;
                }
                else
                {
                    //выполним удаление только в том случае, если пользователь даст согласие на удаление

                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить строку?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        //удаляем выбранную строку

                        db.context.Training.Remove(item);

                        db.context.SaveChanges();

                        MessageBox.Show("Информация удалена");

                        //обновление DataGrid
                        var activeTraining = Convert.ToInt32(StudentsGrid.SelectedValue);
                        var resultTrainingGrid = (from ex in db.context.Exercises
                                      join t in db.context.Training on ex.IdExercises equals t.ExercisesId
                                      join type in db.context.TypeExercises on ex.TypeExercisesId equals type.IdTypeExercises

                                      where t.ClientId == activeTraining
                                      select new
                                      {
                                          IdExercises = ex.IdExercises,
                                          TypeExercisesName = type.Name,
                                          NameExercises = ex.NameExercises,
                                          RegularityExercises = ex.RegularityExercises,
                                          Progress = t.Progress
                                      }).ToList();
                        TrainingGrid.ItemsSource = resultTrainingGrid;

                    }

                }
            }

            catch (Exception)

            {
                MessageBox.Show("Данные не удалены. ");
            }
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

     

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewTraningGrid.Visibility = Visibility.Hidden;
        }

        private void AddNewTraningButon_Click(object sender, RoutedEventArgs e)
        {
            AddNewTraningGrid.Visibility = Visibility.Visible;
            arrayTypeExercises = db.context.TypeExercises.ToList();
            TypeTrainingComboBox.ItemsSource = arrayTypeExercises;
            TypeTrainingComboBox.DisplayMemberPath = "Name";
            TypeTrainingComboBox.SelectedValuePath = "IdTypeExercises";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                TrainerSaveCheck newObject = new TrainerSaveCheck();
                bool result = newObject.CheckAddNewWorkoutsPage(NameTrainingTextBox.Text, Convert.ToString(TypeTrainingComboBox.SelectedValue),
                    DurationTextBox.Text, ReplaysTextBox.Text,
                    QuantityTextBox.Text, RegularityExercisesTextBox.Text
                    );

                if (result)
                {


                    Exercises exercises = new Exercises
                    {
                        NameExercises = NameTrainingTextBox.Text,
                        TypeExercisesId = Convert.ToInt32(TypeTrainingComboBox.SelectedValue),
                        Duration = Convert.ToInt32(DurationTextBox.Text),
                        Replays = Convert.ToInt32(ReplaysTextBox.Text),
                        Quantity = Convert.ToInt32(QuantityTextBox.Text),
                        RegularityExercises = RegularityExercisesTextBox.Text

                    };


                    db.context.Exercises.Add(exercises);
                    db.context.SaveChanges();

                    MessageBox.Show("Добавление выполнено успешно !",
                    "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllTrainingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
