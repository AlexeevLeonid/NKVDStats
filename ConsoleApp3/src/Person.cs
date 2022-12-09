namespace NKVDStats.src
{
    public class Person
    {
        public string Name { get; set; }
        public string Ethn { get; set; }
        public string BPlace { get; set; }
        public string BYear { get; set; }
        public string CPYear { get; set; }
        public string DCause { get; set; }
        public string DPlace { get; set; }

        public Person(string line)
        {
            var data = line.Split("\t");
            Name = data[0].Split(",")[0];
            //if (Name.Length > 3)
            //    Name = Name.Substring(Name.Length - 3);
            if (data[1] == "") Ethn = "не указано";
            else Ethn = data[1];
            BPlace = data[2].Split("обл")[0]; ;
            if (BPlace == "") BPlace = "неизвестно";
            BYear = data[3];
            CPYear = data[4];
            DCause = data[5];
            DPlace = data[6];
        }
    }
}
