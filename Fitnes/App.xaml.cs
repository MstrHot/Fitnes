using Fitnes.Model;
using Microsoft.Build.BuildEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Fitnes
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        Core db = new Core();
        
        public static Users CurrentUserType { get; set; } = null;
        public static Users CurrentUser { get; set; } = null;
        public static Pol CurrentPol { get; set; } = null;
        public static Client CurrentClient { get; set; } = null;
        public static Trainer CurrentTrainer { get; set; } = null;
        public static Users CurrentTrainerId { get; set; } = null;
      
        //public string FIO => Name + " " + Surname[0] + "." + Patronymic[0] + ".";
    }
}
