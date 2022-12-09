using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKVDStats.src
{
    internal class Researcher
    {
        public void GetEthn()
        {
            EthnStat result = new EthnStat();
            Person person;
            string BPlace;
            PersonReader reader = new PersonReader();
            while ((person = reader.GetNext()) is not null)
                //if (person.DCause == "ВМН")
                    result.Add(person);
            result.WriteAllStats();
        }
    }
}
