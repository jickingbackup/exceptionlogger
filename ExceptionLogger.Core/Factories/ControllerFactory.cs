using ExceptionLogger.Core.Abstracts;
using ExceptionLogger.Core.Controllers;
using ExceptionLogger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger.Core.Factories
{
    public class ControllerFactory
    {
        //TODO: FETCH FROM APP CONFIG
        static string connectionString = "mongodb://localhost:27017";

        public static IController<ExceptionLog> CreateExceptionLogController()
        {
            return new ExceptionLogController(connectionString);
        }
    }
}
