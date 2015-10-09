using ExceptionLogger.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExceptionLogger.Website.Models
{
    /// <summary>
    /// DATA TRANSPORT OBJECT for ExceptionLog
    /// </summary>
    public class ExceptionLogDTO
    {

        public string Id { set; get; }
        public string CID { get; set; }
        public string URL { get; set; }
        public string Referer { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string Server { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public bool Is404Error { get; set; }
        public DateTime Date { get; set; }

    }
}