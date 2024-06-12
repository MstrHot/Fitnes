using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fitnes.Model
{
    public partial class Users
    {
        public  string FIO => Name + " " + Surname[0] + "." + Patronymic[0] + ".";
    }
    public partial class View_RequstStuts 
    {
        public string FIOViewRequstStuts=> Name + " " + Surname[0] + "." + Patronymic[0] + ".";
    }
    public partial class ViewTraining
    {
        public string FIOViewViewTraining => Name + " " + Surname[0] + "." + Patronymic[0] + ".";
    }

}
