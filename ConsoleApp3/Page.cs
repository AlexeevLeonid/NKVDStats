namespace NKVDStats
{
    class Page
    {
        public static HttpClient client = new HttpClient();

        public static string page;

        public int currentPos = 0;
        static StreamWriter sw = new StreamWriter("C:\\Users\\leoni\\source\\repos\\ConsoleApp3\\ConsoleApp3\\persons.txt");
        static List<string> urls;
        static string url = "https://nkvd.memo.ru/index.php?title=%D0%A1%D0%BB%D1%83%D0%B6%D0%B5%D0%B1%D0%BD%D0%B0%D1%8F:CargoQuery&limit=20&offset=40000&tables=persons&fields=_pageName%3D%D0%A1%D1%82%D0%B0%D1%82%D1%8C%D1%8F%2Cgender%3Dgender%2Cethn%3Dethn%2Cethn_addendum%3Dethn+addendum%2Cb_date%3Db+date%2Cb_place%3Db+place%2Cb_addendum%3Db+addendum%2Cd_date%3Dd+date%2Cd_place%3Dd+place%2Cd_cause%3Dd+cause%2Cd_addendum%3Dd+addendum%2Ckp_state%3Dkp+state%2Ckp_cand%3Dkp+cand%2Ckp_in%3Dkp+in%2Ckp_out%3Dkp+out%2Ckp_addendum%3Dkp+addendum%2Cksm_state%3Dksm+state%2Cksm_in%3Dksm+in%2Cksm_out%3Dksm+out%2Cksm_addendum%3Dksm+addendum%2Cbund%3Dbund%2Cbund_addendum%3Dbund+addendum%2Caddendum%3Daddendum%2Cdifference%3Ddifference&max+display+chars=300";

        public void Run()
        {
            GetUrls();
            var count = 0;
            foreach (string url in urls)
            {
                GetPage(url);
                Console.SetCursorPosition(1, 1);
                Console.WriteLine("Обрабатывается страница номер \t : " + count++);
                GetDataFromPage();
                
            }
            sw.Close();
        }

        public async Task<string> getPageAs(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();

        }

        public void GetPage(string url)
        {
            var response = getPageAs(url);
            page = response.Result;
        }

        public static void GetUrls()
        {
            urls = new List<string>();
            for (int i = 0; i < 42000; i = i + 5000)
                urls.Add("https://nkvd.memo.ru/index.php?title=%D0%A1%D0%BB%D1%83%D0%B6%D0%B5%D0%B1%D0%BD%D0%B0%D1%8F:CargoQuery&limit=5000&offset=" + i + "&tables=persons&fields=_pageName%3D%D0%A1%D1%82%D0%B0%D1%82%D1%8C%D1%8F%2Cgender%3Dgender%2Cethn%3Dethn%2Cethn_addendum%3Dethn+addendum%2Cb_date%3Db+date%2Cb_place%3Db+place%2Cb_addendum%3Db+addendum%2Cd_date%3Dd+date%2Cd_place%3Dd+place%2Cd_cause%3Dd+cause%2Cd_addendum%3Dd+addendum%2Ckp_state%3Dkp+state%2Ckp_cand%3Dkp+cand%2Ckp_in%3Dkp+in%2Ckp_out%3Dkp+out%2Ckp_addendum%3Dkp+addendum%2Cksm_state%3Dksm+state%2Cksm_in%3Dksm+in%2Cksm_out%3Dksm+out%2Cksm_addendum%3Dksm+addendum%2Cbund%3Dbund%2Cbund_addendum%3Dbund+addendum%2Caddendum%3Daddendum%2Cdifference%3Ddifference&max+display+chars=300");
        }

        public void GetDataFromPage()
        {
            currentPos = 0;
            var count = 1;
            while (GetNextPos() > 0)
            {
                sw.WriteLine(GetData());
                Console.SetCursorPosition(1, 2);
                Console.WriteLine("Обрабатывается запись номер \t : " + count++ + " / 5000");
            }
        }

        public int GetNextPos()
        {
            int fieldPos = page.IndexOf("\"field_Статья\"", currentPos + 1);
            if (fieldPos > -1)
            {
                currentPos = page.IndexOf("title=\"", fieldPos);
                return currentPos;
            }
            return -1;
        }

        public string GetName()
        {
            string res;

            res = page.Substring(currentPos + 7, page.IndexOf("\">", currentPos) - currentPos - 7);

            return res;
        }

        public string GetField(string field)
        {
            int posField = page.IndexOf(field, currentPos);
            return page.Substring(posField + field.Length, page.IndexOf("</td>", posField) - posField - field.Length);
        }

        public string GetEthn()
        {
            return GetField("\"field_ethn\">");
        }

        public string GetBPlace()
        {
            return GetField("\"field_b_place\">");
        }

        public string GetBYear()
        {
            var field = "\"field_b_date\" data-sort-value=\"";
            int posField = page.IndexOf(field, currentPos);
            if (posField > 0)
                return page.Substring(posField + field.Length, page.IndexOf("\">", posField) - posField - field.Length);
            return "";
        }

        public string GetYearCp()
        {
            var field = "\"field_d_date\" data-sort-value=\"";
            int posField = page.IndexOf(field, currentPos);
            if (posField > 0)
                return page.Substring(posField + field.Length, page.IndexOf("\">", posField) - posField - field.Length);
            else return "";
        }

        public string GetIsShot() { 
            return GetField("\"field_d_cause\">");
        }

        public string GetDPlace()
        {
            return GetField("\"field_d_place\">");
        }

        public string GetData()
        {
            string res = "";
            res += GetName() + "\t";
            res += GetEthn() + "\t";
            res += GetBPlace() + "\t";
            res += GetBYear() + "\t";
            res += GetYearCp() + "\t";
            res += GetIsShot() + "\t";
            res += GetDPlace() + "\t";
            return res;
        }

    }
}
