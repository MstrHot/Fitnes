using Fitnes.Model;
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
    /// Логика взаимодействия для AchivPage.xaml
    /// </summary>
    public partial class AchivPage : Page
    {
        Core db = new Core();
        public AchivPage()
        {
            InitializeComponent();
            
            AchivDataGrid.ItemsSource = db.context.Achiev.Where(x => x.TrainerId == App.CurrentTrainer.IdTrainer).ToList();
        }

        private void AchivDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveAchivButton_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                
                db.context.Achiev.AddOrUpdate();
                db.context.SaveChanges();

                MessageBox.Show("Добавление выполнено успешно !",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            }
            catch
            {
                MessageBox.Show("Критический сбор в работе приложения:", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
