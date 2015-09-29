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
        public static ExceptionLogViewModel MapToExceptionLogVM(ExceptionLog log)
        {
            var vm = new ExceptionLogViewModel();

            vm.CID = log.CID;
            vm.Id = log.Id.ToString();
            vm.IP = log.IP;
            vm.Referer = log.Referer;
            vm.URL = log.URL;

            return vm;
        }

    }
}