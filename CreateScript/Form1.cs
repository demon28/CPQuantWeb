using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateScript
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



            StringBuilder builder = new StringBuilder();

            builder.Append("[");

            for (var i = 0; i < 10; i++)
            {

                for (var j = 0; j < 10; j++)
                {

                    for (var k = 0; k < 10; k++)
                    {

                        for (var l = 0; l < 10; l++)
                        {

                            for (var z = 0; z < 10; z++)
                            {

                                var number = " { 'n1':'"+i+ "', 'n2':'" + j + "','n3':'" + k + "','n4':'" + l + "','n5':'" + z + "','code':'" + i + "," + j + "," + k + "," + l + "," + z + "'},";
                                builder.AppendLine(number);
                            }
                        }
                    }
                }

            }
          
            builder.Append("]");

            string s = builder.ToString();
            this.textBox1.Text = s;
        }
    }
}
