using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPQuantWeb.Entites
{
    public class NumberModel
    {
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int N3 { get; set; }
        public int N4 { get; set; }
        public int N5 { get; set; }

        public void Load(string numString)
        {
            string[] list = numString.Split(',');
            this.N1 = int.Parse(list[0]);
            this.N2 = int.Parse(list[1]);
            this.N3 = int.Parse(list[2]);
            this.N4 = int.Parse(list[3]);
            this.N5 = int.Parse(list[4]);
        }

        public string GetString()
        {
            return N1 + "," + N2 + "," + N3 + "," + N4 + "," + N5;
        }

        public int GetInt()
        {
            return int.Parse(N1 + "" + N2 + "" + N3 + "" + N4 + "" + N5);
        }

    }
}
