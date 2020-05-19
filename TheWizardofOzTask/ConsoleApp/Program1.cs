using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using ClassLibrary;

namespace ConsoleApp
{
    class Program1
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            char sep = Path.DirectorySeparatorChar;
            string path = $"data.txt";
            int N = GetInt();
            List<Street> streets = new List<Street>();
            if (CheckStreetDataFile(path))
            {
                var streetsData = File.ReadAllLines(path);

                foreach (var street in streetsData)
                {
                    string[] str = street.Split(new char[] { ' ' });
                    int temp = 0;
                    var nums = (from t in str
                                where int.TryParse(t, out temp)
                                select temp).ToList();
                    try
                    {

                    streets.Add(new Street(str[0], nums.ToArray()));
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
            }
            else
                FillStreetData(streets, N);
            if (streets.Count < N)
                FillStreetData(streets, N - streets.Count);

            foreach (var street in streets)
                Console.WriteLine(street);

            SerializeStreets("out.txt", streets);
        }

        private static void SerializeStreets(string path, List<Street> streets)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Street>));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, streets);
                Console.WriteLine("Serialization completed successfully!");
            }
        }

        private static void FillStreetData(List<Street> streets, int n)
        {
            for (int i = 0; i < n; i++)
                streets.Add(new Street(GenerateName(), GenerateIntArr()));
        }

        static string GenerateName()
        {
            int len = rnd.Next(3, 8);
            var sb = new StringBuilder();
            sb.Append((char)rnd.Next('A', 'Z' + 1));
            for (int i = 0; i < len; i++)
                sb.Append((char)rnd.Next('a', 'z' + 1));

            return sb.ToString();
        }

        static int[] GenerateIntArr()
        {
            var nums = new List<int>();
            int len = rnd.Next(1, 11);

            for (int i = 0; i < len; i++)
                nums.Add(rnd.Next(1, 101));

            return nums.ToArray();
        }

        static int GetInt()
        {
            int n = 0;
            while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 1000)
            {
                Console.Clear();
                Console.WriteLine("Incorret input");
            }
            Console.Clear();
            Console.WriteLine(n + " streets");
            return n;
        }

        static bool CheckStreetDataFile(string path)
        {
            if (!File.Exists(path))
                return false;

            var streetsData = File.ReadAllLines(path);

            if (streetsData.Length == 0)
                return false;
            bool validData = true;

            foreach (var street in streetsData)
            {
                string[] str = street.Split(new char[] { ' ' });
                if (str.Length < 2)
                    return false;
                int temp = 0;
                var nums = (from t in str
                            where int.TryParse(t, out temp)
                            select t).ToList();

                if (nums.Count != str.Length - 1)
                {
                    validData = false;
                    break;
                }
            }

            return validData;
        }
    }

}
