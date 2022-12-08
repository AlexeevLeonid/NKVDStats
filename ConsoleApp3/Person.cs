using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKVDStats
{
    public class Person
    {
        public string nat { get; set; }
        public string surname { get; set; }
        public string yearOfService { get; set; }

        public string birthPlace { get; set; }

        public Person(string n, string s, string y, string b)
        {
            if (n is not null)
                nat = n;
            else nat = "Неизвестно";
            surname = s;
            yearOfService = y;

            if (b is not null)
                birthPlace = b;
            else birthPlace = "Неизвестно";
        }
    }
}
