using SetsProcessor.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Init
{
    class Program
    {
        static void Main(string[] args)
        {

            Functions func = new Functions();

            func.AddSet("1,2,3");
            func.AddSet("1,2,3,4");
            func.AddSet("2,1,3");
            func.AddSet("1,,3");
            func.AddSet("2,3");
            func.AddSet("1,2,3,");

            Console.WriteLine(func.GetMoreFrequentSet());
            Console.WriteLine(func.GetIncorrectSets());
            Console.WriteLine(func.Resume());

        }

    }
}
