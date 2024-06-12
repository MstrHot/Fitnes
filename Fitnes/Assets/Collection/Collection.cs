using Fitnes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fitnes.Assets.Collection
{

    public class Collection
    {
        static ObservableCollection<string> _collectionTrainingDetails;
        public static ObservableCollection<string> collectionTrainingDetails
        {
            get => _collectionTrainingDetails ?? (_collectionTrainingDetails = new ObservableCollection<string>());
            private set => _collectionTrainingDetails = value;
        }

        static ObservableCollection<string> _listWords;
        /// <summary>Список слов</summary>
        public static ObservableCollection<string> ListWords
        {
            get => _listWords ?? (_listWords = new ObservableCollection<string>());
            private set => _listWords = value;
        }

        /// <summary>Сохранение списка слов в файле</summary>
        /// <param name="NameFile">Имя файла</param>
        public static void Save(string NameFile = null)
        {
            if (string.IsNullOrWhiteSpace(NameFile))
                NameFile = "ListWords.txt";
            StreamWriter file = null;
            try
            {
                file = new StreamWriter(NameFile, false, Encoding.Default);
                foreach (string word in ListWords)
                    file.WriteLine(word.ToLower());
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка записи в файл!");
            }
            if (file != null)
                file.Close();
        }

        /// <summary>Чтение списка слов из файла</summary>
        /// <param name="NameFile">Имя файла</param>
        public static void Load(string NameFile = null)
        {
            if (string.IsNullOrWhiteSpace(NameFile))
            {
                NameFile = "ListWords.txt";
                if (!File.Exists(NameFile))
                    File.Create(NameFile).Close();
            }
            StreamReader file = null;
            try
            {
                file = new StreamReader(NameFile, Encoding.Default);
                ListWords = new ObservableCollection<string>(file.ReadToEnd().Split("\r\n ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(wrd => wrd.ToLower()));
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка чтения из файла!");
            }
            if (file != null)
                file.Close();
        }

        /// <summary>Добавление слова в список</summary>
        /// <param name="Words">Слово или список слов разделённых переносом строки или пробелом</param>
        public static void AddWord(string Words)
        {
            foreach (string word in Words.Split("\r\n ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                ListWords.Add(word);
        }

        /// <summary>Поиск слова в списке</summary>
        /// <param name="Word">Одно слово</param>
        /// <returns>Результат поиска</returns>
        public static string SearchWord(string Word)
        {
            string[] wordArr = Word.ToLower().Split("\r\n ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (wordArr.Length != 1) return "Ошибка ввода";
            Word = wordArr[0];
            int index = ListWords.IndexOf(Word);
            if (index >= 0) return "Tочное совпадение со словом по индексу " + index;
            /// *************************
            /// Нечёткий поиск
            /// *************************
            return "Результат нечёткого поиска";
        }
    }


   
    }
