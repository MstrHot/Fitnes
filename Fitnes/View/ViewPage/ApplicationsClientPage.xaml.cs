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
    /// Логика взаимодействия для ApplicationsClientPage.xaml
    /// </summary>
    public partial class ApplicationsClientPage : Page
    {
        Core db = new Core();


        public ApplicationsClientPage()
        {
            InitializeComponent();
            var resultOrder = (from r in db.context.Request
                              join t in db.context.Trainer on r.TrenerId equals t.IdTrainer
                              join s in db.context.Stuts on r.StatusId equals s.IdStuts
                              join c in db.context.Client on  r.ClientId equals c.IdClient
                              join target in db.context.Target on c.Target equals target.IdTarget
                              join u in db.context.Users on c.UserId equals u.IdUsers
                               where t.IdTrainer == App.CurrentTrainer.IdTrainer
                              select new { IdRequest = r.IdRequest, Type = s.Type , Target = target.TargetName , IdTrainer=t.IdTrainer , FIO =u.Surname }).ToList();

            
            OrderListView.ItemsSource = resultOrder;
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            // Извлекаем значение IdRequest
            var selectedItem = OrderListView.SelectedItems[0] as dynamic;
            int selectedRequest = selectedItem.IdRequest;
        
            App.CurrentRequest = db.context.Request.FirstOrDefault(x => x.IdRequest == selectedRequest);
        }
        private void AcceptApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            try

            {

                App.CurrentRequest.StatusId = 1;
                db.context.Request.AddOrUpdate(App.CurrentRequest);
                db.context.SaveChanges();

                MessageBox.Show("Принятие заявки выполнено успешно !",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            }
            catch
            {
                MessageBox.Show("Критический сбор в работе приложения:", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeclineApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            try

            {

                App.CurrentRequest.StatusId = 2;
                db.context.Request.AddOrUpdate(App.CurrentRequest);
                db.context.SaveChanges();

                MessageBox.Show("Отказ заявки выполнено успешно !",
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
