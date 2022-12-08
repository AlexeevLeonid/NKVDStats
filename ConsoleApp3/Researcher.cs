using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKVDStats
{
    internal class Researcher
    {
        //StreamReader sr = new StreamReader("C:\\Users\\leoni\\source\\repos\\ConsoleApp3\\ConsoleApp3\\persons.txt");
        static StreamWriter sw = new StreamWriter("C:\\Users\\leoni\\source\\repos\\ConsoleApp3\\ConsoleApp3\\Ethn.txt");

        public void GetEthn()
        {
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\leoni\\source\\repos\\ConsoleApp3\\ConsoleApp3\\persons.txt");
                var s = sr.ReadLine();
                Dictionary<string, EthnStat> result = new Dictionary<string, EthnStat>();
                string surname, ethn, BPlace, line;
                string[] person;
                var count = 0;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    person = line.Split("\t");
                    surname = person[0].Split(",")[0];
                    ethn = person[1];
                    if (ethn == "") ethn = "не указано";
                    BPlace = person[2].Split("обл")[0];
                    if (result.ContainsKey(ethn))
                        result[ethn].Add(surname, BPlace);
                    else
                        result.Add(ethn, new EthnStat(surname, BPlace));
                    Console.SetCursorPosition(1, 2);
                    Console.WriteLine("Обрабатывается запись номер \t : " + count++ + " / ~42000");
                }
                foreach (var i in result)
                {
                    sw.WriteLine(i.Key + "\t" + i.Value.count);
                }
                sw.WriteLine("======================БОЛЕЕ ТОЧНАЯ ИНФОРМАЦИЯ========================");
                foreach (var i in result)
                {
                    sw.WriteLine(i.Key);
                    sw.WriteLine(i.Value);
                }
            } catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }


    public class EthnStat
    {
        public int count { get; set; }
        public Dictionary<string, int> BPlaces { get; set; }
        public Dictionary<string, int> Surnames { get; set; }

        public EthnStat(String firstSurame, string firstBPlace)
        {
            count = 1;
            BPlaces =  new Dictionary<string, int>();
            Surnames = new Dictionary<string, int>();
            BPlaces[firstBPlace] = 1;
            Surnames[firstSurame] = 1;
        }

        public void Add(String surname, string bPlace)
        {
            count++;
            if (Surnames.ContainsKey(surname))
                Surnames[surname]++;
            else Surnames.Add(surname, 1);
            if (BPlaces.ContainsKey(bPlace))
                BPlaces[bPlace]++;
            else BPlaces.Add(bPlace, 1);
        }

        public override string ToString()
        {
            string res = "Всего - " + count + "\nМеста Рождения";
            foreach (var item in BPlaces)
                res += "\n\t" + item.Key + " : " + item.Value;
            res += "\nФамилии";
            foreach (var item in Surnames)
                res += "\n\t" + item.Key + " : " + item.Value;
            return res;
        }

    }

}
