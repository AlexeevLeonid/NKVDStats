using System.Runtime.InteropServices;
using System.Text;

namespace NKVDStats.src
{
    public class EthnStat
    {
        static StreamWriter sw = new StreamWriter("C:\\Users\\leoni\\source\\repos\\ConsoleApp3\\ConsoleApp3\\results\\Этнический состав растрелянных.txt");

        public static Dictionary<string, EthnStat> All = new Dictionary<string, EthnStat>();
        public string EthnName;
        public int count { get; set; }
        public Dictionary<string, int> BPlaces { get; set; }
        public Dictionary<string, int> Surnames { get; set; }

        public EthnStat()
        {
        }
        public EthnStat(Person person)
        {
            EthnName = person.Ethn;
            count = 1;
            BPlaces = new Dictionary<string, int>();
            Surnames = new Dictionary<string, int>();
            BPlaces[person.BPlace] = 1;
            Surnames[person.Name] = 1;
            All.Add(EthnName, this);
        }

        public void Add(Person person)
        {
            if (All.ContainsKey(person.Ethn))
            {
                EthnStat t = All[person.Ethn];
                t.count++;
                if (t.Surnames.ContainsKey(person.Name))
                    t.Surnames[person.Name]++;
                else t.Surnames.Add(person.Name, 1);
                if (t.BPlaces.ContainsKey(person.BPlace))
                    t.BPlaces[person.BPlace]++;
                else t.BPlaces.Add(person.BPlace, 1);
            }
            else new EthnStat(person);
        }

        public void WriteAllStats()
        {
            foreach (var item in All.OrderByDescending(i => i.Value.count))
            {
                sw.WriteLine(item.Key + Tabb(item.Key.Length, 5) + item.Value.count);
            }
            sw.WriteLine("\n\n\n======================БОЛЕЕ ТОЧНАЯ ИНФОРМАЦИЯ========================\n\n\n");
            foreach (var item in All.OrderByDescending(i => i.Value.count))
            {
                sw.WriteLine(item.Key);
                sw.WriteLine(item.Value.GetStatToString());
            }

        }

        public string GetStatToString()
        {
            var count1 = 0;
            var count2 = 0;
            StringBuilder res = new StringBuilder("Всего - " + count + "\nМеста Рождения");
            foreach (var item in BPlaces.OrderByDescending(i => i.Value))
                if (item.Value > 3)
                    res.Append("\n\t" + item.Key + Tabb(item.Key.Length, 5) + item.Value + "\t\t" + item.Value*100/count + "%" );
                else count1++;
            res.Append("\n\tМеньше 4 нквдшников\t : " + count1);
            res.Append("\nФамилии");
            foreach (var item in Surnames.OrderByDescending(i => i.Value))
                if (item.Value > 3)
                    res.Append("\n\t" + item.Key + Tabb(item.Key.Length, 5) + item.Value + "\t\t" + item.Value * 100 / count + "%");
                else count2++;
            res.Append("\n\tМеньше 4 нквдшников\t\t : " + count2 + "\n");
            return res.ToString();
        }

        public static bool operator >(EthnStat e1, EthnStat e2)
        {
            return e1.count > e2.count;
        }

        public static bool operator <(EthnStat e1, EthnStat e2)
        {
            return e1.count < e2.count;
        }

        public string Tabb(int s, int count)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 1; i < count - s / 4; i++)
            {
                sb.Append("\t");
            }
            return sb.ToString();
        }
    }
}
