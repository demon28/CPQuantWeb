using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs;
namespace CPQuantWeb.Entites
{
    public class JSHelper
    {

        public List<NumberModel> RunCeKvScript(string code)
        {


            return null;

        }


        public static async void RunJSAsync()
        {



            var func = Edge.Func(@"
            function fun(data) {
                retrun ;
            }
            fun('near');

            ");


            var res= await func(".NET");

         
        }

    }
}
