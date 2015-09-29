using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger.Core.Models
{
    public class ExceptionLog
    {
        [BsonElementAttribute("_id")]
        public ObjectId Id { set; get; }
        [BsonElementAttribute("cid")]
        public string CID { get; set; }
        [BsonElementAttribute("url")]
        public string URL { get; set; }
        [BsonElementAttribute("referer")]
        public string Referer { get; set; }
        [BsonElementAttribute("ip")]
        public string IP { get; set; }
        [BsonElementAttribute("browser")]
        public string Browser { get; set; }
        [BsonElementAttribute("server")]
        public string Server { get; set; }
        [BsonElementAttribute("exception")]
        public string Exception { get; set; }
        [BsonElementAttribute("stacktrace")]
        public string StackTrace { get; set; }
        [BsonElementAttribute("is404error")]
        public bool Is404Error { get; set; }
        [BsonElementAttribute("date")]
        public DateTime Date { get; set; }
    }
}
