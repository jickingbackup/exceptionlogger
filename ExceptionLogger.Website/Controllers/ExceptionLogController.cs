using ExceptionLogger.Core.Abstracts;
using ExceptionLogger.Core.Factories;
using ExceptionLogger.Core.Models;
using ExceptionLogger.Website.Factories;
using ExceptionLogger.Website.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ExceptionLogger.Website.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class ExceptionLogController : ApiController
    {

        private IController<ExceptionLog> controller = null;

        public ExceptionLogController()
        {
            controller = ControllerFactory.CreateExceptionLogController();
        }

        // GET api/values
        public IEnumerable<ExceptionLogViewModel> Get()
        {
            var list = controller.Get().ToList();
            //var filteredList = new List<ExceptionLog>();
            List<ExceptionLogViewModel> result = new List<ExceptionLogViewModel>();

            #region FILTERS
            //DATE
            if (HttpContext.Current.Request.QueryString["date"] != null)
            {
                DateTime date = DateTime.Now;
                DateTime.TryParse(HttpContext.Current.Request.QueryString["date"].ToString(), out date);
                list = list.Where(x => 
                        x.Date.ToShortDateString() == date.ToShortDateString()
                    ).ToList();
            }
            //MIN DATE
            if (HttpContext.Current.Request.QueryString["date"] == null && HttpContext.Current.Request.QueryString["mindate"] != null)
            {

            }
            //MAXDATE
            if (HttpContext.Current.Request.QueryString["date"] == null && HttpContext.Current.Request.QueryString["maxdate"] != null)
            {

            }
            //ERROR FILTER
            if (HttpContext.Current.Request.QueryString["error"] != null)
            {
                if (HttpContext.Current.Request.QueryString["error"] == "404")
                {
                    list = list.Where(x =>
                            x.Is404Error == true
                        ).ToList();
                }
                else
                {
                    list = list.Where(x =>
                            x.Is404Error == false
                        ).ToList();
                }
            }
            //keyword
            if (HttpContext.Current.Request.QueryString["keyword"] != null)
            {
                string keyword = HttpContext.Current.Request.QueryString["keyword"];
                list = list.Where(x =>
                        x.URL.ToLower().Contains(keyword)
                        || x.StackTrace.ToLower().Contains(keyword)
                        || x.Server.ToLower().Contains(keyword)
                        || x.Referer.ToLower().Contains(keyword)
                        || x.IP.ToLower().Contains(keyword)
                        || x.Exception.ToLower().Contains(keyword)
                        || x.CID.ToLower().Contains(keyword)
                        || x.Browser.ToLower().Contains(keyword)
                    ).ToList();
            }
            #endregion


            foreach (var item in list)
            {
                var i = ViewModelFactory.MapToExceptionLogVM(item);

                result.Add(i);
            }

            return result;
        }

        //GET api/values/5
        public ExceptionLogViewModel Get(string id)
        {
            var exceptionLog = controller.Get(id);
            ExceptionLogViewModel result = null;

            if(exceptionLog == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            result = ViewModelFactory.MapToExceptionLogVM(exceptionLog);
            return result;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete([FromBody]int id)
        {
        }
    }
}
