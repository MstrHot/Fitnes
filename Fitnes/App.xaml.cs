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
        
       
        public static Users CurrentUser { get; set; } = null;
        public static Client CurrentClient { get; set; } = null;
        public static Trainer CurrentTrainer { get; set; } = null;
        public static View_TrainingDetails CurrentTrainingDetails { get; set; } = null;
        public static Request CurrentRequest { get; set; } = null;
        public static Exercises CurrentExercises { get; set; } = null;
        public static Training CurrentTraining { get; set; } = null;
        public static Achiev CurrentAchiev { get; set; } = null;


    }
}
