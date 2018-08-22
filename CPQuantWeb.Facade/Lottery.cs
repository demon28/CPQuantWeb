using CPQuantWeb.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Winner.Framework.Utils;

namespace CPQuantWeb.Facade
{
    public class Lottery
    {
        public string GetLastNumber(int cid, int count)
        {
            CPQuantWeb.DataAccess.Tcp_HiscodeCollection tcp = new DataAccess.Tcp_HiscodeCollection();
            string sql = "SELECT s.* FROM (SELECT t.*,rownum FROM  tcp_hiscode t WHERE t.cid >" + cid + " ORDER BY t.cid ASC) s WHERE rownum < " + count;

            if (tcp.ListBySQL(sql))
            {
                List<NumberModel> numbers = new List<NumberModel>();

                for (int i = 0; i < tcp.DataTable.Rows.Count; i++)
                {
                    NumberModel number = new NumberModel();
                    string[] sl= tcp.DataTable.Rows[i]["opencode"].ToString().Split(',');
                    number.N1 = int.Parse(sl[0]);
                    number.N2 = int.Parse(sl[1]);
                    number.N3 = int.Parse(sl[2]);
                    number.N4 = int.Parse(sl[3]);
                    number.N5 = int.Parse(sl[4]);
                    numbers.Add(number);
                }

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                jsSerializer.MaxJsonLength = Int32.MaxValue;

                return jsSerializer.Serialize(numbers);

            }
            return "[]";
        }

        public string GetBaseNumber()
        {
            List<NumberModel> numbers = new List<NumberModel>();

            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    for (int k = 0; k < 10; k++)
                    {

                        for (int l = 0; l < 10; l++)
                        {

                            for (int z = 0; z < 10; z++)
                            {
                                NumberModel number = new NumberModel();
                                number.N1 = i;
                                number.N2 = j;
                                number.N3 = k;
                                number.N4 = l;
                                number.N5 = z;

                                numbers.Add(number);
                            }
                        }
                    }
                }
            }
         
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;

            return jsSerializer.Serialize(numbers);
        }
    }
}
