using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnitTests.UndoRedo;

namespace UnitTests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            List<Test> tests = new List<Test>();
            //tests.Add(new CheckProjectDuplication());

            for (int index = 0; index < tests.Count; index++)
            {
                Test test = tests[index];
                Console.Write("Executing test " + (index + 1) + "/" + tests.Count + ":  ");

                try
                {
                    test.Initialize();
                    test.Execute();
                    Console.WriteLine("Success");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failure.  " + e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
