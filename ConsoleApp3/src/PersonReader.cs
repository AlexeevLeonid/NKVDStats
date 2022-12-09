namespace NKVDStats.src
{
    public class PersonReader
    {
        StreamReader sr = new StreamReader("C:\\Users\\leoni\\source\\repos\\ConsoleApp3\\ConsoleApp3\\results\\persons.txt");
        int count = 0;
        public Person GetNext()
        {

            try
            {
                if (!sr.EndOfStream)
                {
                    Console.SetCursorPosition(1, 2);
                    Console.WriteLine("Обрабатывается запись номер \t : " + count++ + " / ~42000");
                    return new Person(sr.ReadLine());
                }
                sr.Close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }
        }
    }
}
