using ExceptionLogger.Core.Abstracts;
using ExceptionLogger.Core.Controllers;
using ExceptionLogger.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger.Core.Factories
{
    public class ControllerFactory
    {
        //TODO: FETCH FROM APP CONFIG
        static string connectionString = "mongodb://localhost:27017";

        public static IController<ExceptionLog> CreateExceptionLogController(string key = null)
        {
            if (key != null)
                connectionString = System.Configuration.ConfigurationManager.AppSettings[key];

            return new ExceptionLogController(connectionString);
        }
    }
}
