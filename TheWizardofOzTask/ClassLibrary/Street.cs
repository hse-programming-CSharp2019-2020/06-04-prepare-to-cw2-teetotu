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
        public string Name { get; private set; }
        private int[] _houses;

        public Street(string name, int[] houseNumbers)
        {
            if (houseNumbers == null || name == null)
                throw new ArgumentNullException();
            Name = name;
            _houses = houseNumbers;
        }

        private int _numberOfHouses => _houses.Length;
        public static int operator ~(Street street) => street._numberOfHouses;
        public static bool operator !(Street street)
        {
            var a = (from h in street._houses
                     where h.ToString().Contains('7')
                     select h).ToList();
            return a.Count != 0;
        }

        public override string ToString()
        {
            if (_houses == null)
                throw new ArgumentNullException("no street initiated");
            var sb = new StringBuilder();
            sb.Append(Name + Environment.NewLine);
            for (int i = 0; i < _houses.Length - 1; i++)
                sb.Append(_houses[i] + ", ");
            sb.Append(_houses[_houses.Length - 1]);
            return sb.ToString();
        }
    }
}
