using Fitnes.Model;
using Fitnes.View.CheckClasses;
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
    /// Логика взаимодействия для AddNewWorkoutsPage.xaml
    /// </summary>
    public partial class AddNewWorkoutsPage : Page
    {
        Core db = new Core();
        List<TypeExercises> arrayTypeExercises;
        public AddNewWorkoutsPage()
        {
            InitializeComponent();

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
                bool result = newObject.CheckAddNewWorkoutsPage(NameTrainingTextBox.Text , Convert.ToString(TypeTrainingComboBox.SelectedValue) , 
                    DurationTextBox.Text,ReplaysTextBox.Text,
                    QuantityTextBox.Text,RegularityExercisesTextBox.Text
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
    }
}
