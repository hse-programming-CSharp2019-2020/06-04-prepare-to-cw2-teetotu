using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassLibrary;
using Microsoft.Win32;

namespace ConsoleApp2
{
    class Program2
    {
        static void Main(string[] args)
        {
            char sep = Path.DirectorySeparatorChar;
            string path = $"..{sep}..{sep}..{sep}ConsoleApp{sep}bin{sep}debug{sep}out.txt";
            if (!File.Exists(path))
                Console.WriteLine("file doesn't exist :(");
            var streets = DeserializeStreets(path);

            var tempName = (from str in streets
                            where ~str % 2 != 0 && !str
                            select str).ToList();
            Console.WriteLine("Looking for magic streets...");
            if (tempName.Count == 0)
                Console.WriteLine("None found");
            else
                foreach (var str in tempName)
                    Console.WriteLine(str);
        }

        static List<Street> DeserializeStreets(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Street>));
            var streets = new List<Street>();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    streets = (List<Street>)formatter.Deserialize(fs);
                    Console.WriteLine("Deserialization completed successfully!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            return streets;
        }
    }
}
