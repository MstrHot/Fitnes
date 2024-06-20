using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fitnes.View.CheckClasses
{
    public class UserSaveCheck
    {
        public  bool TrainingSaveExercises(string Exercises) 
        {
            if (string.IsNullOrEmpty(Exercises))
            {
                throw new Exception("Пустое поле");
            }
            if (!Regex.Match(Exercises, "[^0-9.-]+").Success)
            {

                if (Convert.ToInt32(Exercises) <= App.CurrentTrainingDetails.Replays)
                {
                    return true;

                }
                else
                {
                    throw new Exception("Нельзя ввести больше подходов чем задано в упражнение");
                }
            }
            else
            {
                throw new Exception("Нельзя ввести буквы, Введите число");
            }
           
        }
    }
}
