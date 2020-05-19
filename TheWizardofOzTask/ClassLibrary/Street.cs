using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Street
    {
        public string name;
        public int[] houses;

        public Street() { }

        public Street(string name, int[] houseNumbers)
        {
            if (houseNumbers == null || name == null)
                throw new ArgumentNullException();
            this.name = name;
            houses = houseNumbers;
        }

        private int _numberOfHouses => houses.Length;


        public static int operator ~(Street street) => street._numberOfHouses;
        public static bool operator !(Street street)
        {
            var a = (from h in street.houses
                     where h.ToString().Contains('7')
                     select h).ToList();
            return a.Count != 0;
        }

        public override string ToString()
        {
            if (houses == null)
                throw new ArgumentNullException("no street initiated");
            var sb = new StringBuilder();
            sb.Append(name + Environment.NewLine);
            for (int i = 0; i < houses.Length - 1; i++)
                sb.Append(houses[i] + ", ");
            sb.Append(houses[houses.Length - 1]);
            return sb.ToString();
        }
    }
}
