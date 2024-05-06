using Fitnes.Model;
using Microsoft.Build.BuildEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fitnes
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Users CurrentUser { get; set; } = null;
        public static Pol CurrentPol { get; set; } = null;
        public static Client CurrentClient { get; set; } = null;

    }
}
