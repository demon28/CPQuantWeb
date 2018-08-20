using EdgeJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunJS
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run((Action)Start).Wait();
        }

        public static async void Start()
        {
            var func = Edge.Func(@"
            return function (data, callback) {


                 var list=0;
                 for(var i=0;i<10;i++)
                 {
                                            list=list+1;
                  }
                callback(null, list);
            }
        ");

            Console.WriteLine(await func(".NET"));
        }
    }
}
