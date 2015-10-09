using ExceptionLogger.Core.Models;
using ExceptionLogger.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExceptionLogger.Website.Factories
{
    public class ViewModelFactory
    {
        public static ExceptionLogDTO MapToExceptionLogVM(ExceptionLog log)
        {
            var vm = new ExceptionLogDTO();

            vm.CID = log.CID;
            vm.Id = log.Id.ToString();
            vm.IP = log.IP;
            vm.Referer = log.Referer;
            vm.URL = log.URL;
            vm.Browser = log.Browser;
            vm.Server = log.Server;
            vm.Exception = log.Exception;
            vm.StackTrace = log.StackTrace;
            vm.Is404Error = log.Is404Error;
            vm.Date = log.Date;

            return vm;
        }

    }
}