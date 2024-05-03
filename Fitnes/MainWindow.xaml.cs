using Fitnes.View;
using Fitnes.View.ViewPage;
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

namespace Fitnes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //вывод имени пользователя

            UsersName.Content = App.CurrentUser.Name;
            MainFrame.Navigate(new ProfilePage());

        }
       
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            
            
        }

        private void MyProfile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
            MyProfile.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#10B9BC");
            MyWorkouts.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffff");
            MyCoach.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffff");
        }

        private void MyWorkouts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TrainingPage());
            MyWorkouts.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#10B9BC");
            MyProfile.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffff");
            MyCoach.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffff");

        }

       

        private void MyAchievements_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyCoach_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TrainersPage());
            MyCoach.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#10B9BC");
            MyWorkouts.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffff");
            MyProfile.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffff");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           App.CurrentUser=null;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
