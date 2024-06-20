using Fitnes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fitnes.View.CheckClasses
{
    public class TrainerSaveCheck
    {
        public bool CheckAddNewWorkoutsPage(string NameE , string TypeE , string Duration , string Replays , string Quantity, string Regularity )
        {
            if (String.IsNullOrEmpty(NameE))
            {
                throw new Exception("Не введите название тренировки");
            }
            if(String.IsNullOrEmpty(TypeE))
            {
                throw new Exception("Не выбран тип тренировки");
            }
            if(String.IsNullOrEmpty(Duration))
            {
                throw new Exception("Укажите продолжительность тренировки");
            }
            if (Regex.Match(Duration, "[^0-9.-]+").Success)
            {
                throw new Exception("Нельзя ввести буквы, Введите число в поле Длительность");
            }
                if (String.IsNullOrEmpty(Replays))
            {
                throw new Exception("Укажите количество повторов");
            }
            if (Regex.Match(Replays, "[^0-9.-]+").Success)
            {
                throw new Exception("Нельзя ввести буквы, Введите число в поле Повторы");
            }
            if (String.IsNullOrEmpty(Quantity))
            {
                throw new Exception("Укажите количество за подход");
            }
            if (Regex.Match(Quantity, "[^0-9.-]+").Success)
            {
                throw new Exception("Нельзя ввести буквы, Введите число в поле Количество ");
            }
            if (string.IsNullOrEmpty(Regularity))
            {
                throw new Exception("Укажите количество в неделю");
            }
            if (Regex.Match(Regularity, "[^0-9.-]+").Success)
            {
                throw new Exception("Нельзя ввести буквы, Введите число в поле Количество раз в неделю");
            }
            if(Convert.ToInt32(Regularity) >= 7)
            {
                throw new Exception("Нельзя установить повтороние в неделю больше чем 7");
            }

            return true;
        }
    }
}
