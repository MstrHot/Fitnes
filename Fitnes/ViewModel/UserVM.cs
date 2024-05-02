using Fitnes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnes.ViewModel
{
    public class UserVM
    {
        Core db = new Core();
        public bool CheckAuth(string nunber, string password)
        {
            int checkClient = db.context.Users.Where(x => x.Nunber == nunber && x.Password == password).Count();
            if (String.IsNullOrEmpty(nunber))
            {
                throw new Exception("Не введён логин");
            }
            else if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Не введён пароль");
            }
            else
            {
                if (checkClient == 0)
                {
                    throw new Exception("Пользователь не найден");
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
