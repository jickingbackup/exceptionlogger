using ExceptionLogger.Core.Factories;
using ExceptionLogger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            // generate 100 documents with a counter ranging from 0 - 99
            var documents = Enumerable.Range(0, 4).Select(i => new ExceptionLog() { Browser="xxx",CID = "xxx", Date= DateTime.Now,Exception = "xxx", IP = "xxx", Referer = "xxx", Server = "xxx", StackTrace = "xxx", URL = "xxx"});
            //
            var controller = ControllerFactory.CreateExceptionLogController();

            foreach (var item in documents)
            {
                controller.Add(item);
                count++;
                Console.WriteLine(count);
            }

            Console.ReadLine();
        }
    }
}
