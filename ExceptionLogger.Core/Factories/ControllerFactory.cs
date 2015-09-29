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
        public static IController<ExceptionLog> CreateExceptionLogController()
        {
            return new ExceptionLogController();
        }
    }
}
