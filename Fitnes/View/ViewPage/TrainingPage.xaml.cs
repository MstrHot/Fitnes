using Fitnes.Model;
using Fitnes.View.CheckClasses;
using Fitnes.ViewModel;
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
using static System.Net.Mime.MediaTypeNames;
using Excel = Microsoft.Office.Interop.Excel;

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
        List<Exercises> arryExercises ;
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
            
            try
            {
                UserSaveCheck newObject = new UserSaveCheck();
                bool result = newObject.TrainingSaveExercises(ExercisesTextBox.Text);
               
               if (result)
                {

                    int SearchProgress = Convert.ToInt32(ExercisesTextBox.Text) % (App.CurrentTrainingDetails.Replays % 100) * 10;
                    if(Convert.ToInt32(ExercisesTextBox.Text) == App.CurrentTrainingDetails.Replays)
                    {
                        SearchProgress = 100;
                    }
                Training training = new Training()
                {
                    IdTraining = App.CurrentTrainingDetails.IdTraining,
                    DoneExercises = Convert.ToInt32(ExercisesTextBox.Text),
                    ClientId = App.CurrentClient.IdClient,
                    ExercisesId = App.CurrentTrainingDetails.ExercisesId,
                    TrenerId = App.CurrentTrainingDetails.TrenerId,
                    Progress = SearchProgress
                };






                db.context.Training.AddOrUpdate(training);
                db.context.SaveChanges();

                MessageBox.Show("Сохрание успешно",
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

        private void Inputxercises_Click(object sender, RoutedEventArgs e)
        {
            InputxercisesGrid.Visibility = Visibility.Visible;
            ExercisesTextBox.Text = App.CurrentTrainingDetails.DoneExercises.ToString();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InputxercisesGrid.Visibility = Visibility.Hidden;
        }

      
        private void ExportExceleButton_Click(object sender, RoutedEventArgs e)
        {
           

            /*создаем файл Excel*/

            var aplication = new Excel.Application();

            aplication.Visible = true;

            /*количество листов*/

            aplication.SheetsInNewWorkbook = 1;

            /*добавляем рабочую книгу*/

            Excel.Workbook workbook = aplication.Workbooks.Add(System.Type.Missing);

            /*создаем лист*/

            Excel.Worksheet worksheet = workbook.ActiveSheet;

            worksheet.Name = "Trining"; //имя листа нужно вводить латинскими буквами

            /*заголовки вывод в Excel (в первую строку)*/

            worksheet.Cells[1][1] = "Номер тренировки";
            worksheet.Cells[2][1] = "Вид тренировик";
            worksheet.Cells[3][1] = "Название тренировик";
            worksheet.Cells[4][1] = "Время";
            worksheet.Cells[5][1] = "Подходы";
            worksheet.Cells[6][1] = "Сделано подходов";
            worksheet.Cells[7][1] = "За подход";
            worksheet.Cells[8][1] = "Количество за неделю";
            worksheet.Cells[9][1] = "Прогресс";


            /*вывод данных из массива в Excel*/

            int rowIndex = 2;  //номер строки для вывода данных из массива

            foreach (var item in arryExercises)

            {

                worksheet.Cells[1][rowIndex] = item.IdExercises;
                worksheet.Cells[2][rowIndex] = item.TypeExercisesId;
                worksheet.Cells[3][rowIndex] = item.NameExercises;
                worksheet.Cells[4][rowIndex] = item.Duration;
                worksheet.Cells[5][rowIndex] = item.Replays;
                worksheet.Cells[6][rowIndex] = item.Quantity;
                worksheet.Cells[7][rowIndex] = item.RegularityExercises;
                worksheet.Cells[8][rowIndex] = item.RegularityExercises;
                worksheet.Cells[9][rowIndex] = App.CurrentTraining.Progress;

                rowIndex++;

            }
        }
    }
}
