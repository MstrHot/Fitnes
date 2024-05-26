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
    /// Логика взаимодействия для TrainingPage.xaml
    /// </summary>
    public partial class TrainingPage : Page
    {
        Core db = new Core();
        public TrainingPage()
        {
           
            InitializeComponent();
            TrainingGrid.ItemsSource = db.context.ViewTraining.Where(x=> x.ClientId== App.CurrentClient.IdClient).ToList();
            TrainingGrid.SelectedValuePath = "IdTraining";
        }

        private void TrainingGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void DelTraning_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTraning_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TrainingGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrainingAllStackPanel.Visibility = Visibility.Hidden;
            TrainingDetailsStackPanel.Visibility = Visibility.Visible;
            int activeTraining = Convert.ToInt32(TrainingGrid.SelectedValue);
            Console.WriteLine(activeTraining);
            TrainingDetailsGrid.ItemsSource = db.context.View_TrainingDetails.Where(x=>x.IdTraining == activeTraining).ToList();


        }
    }
}
