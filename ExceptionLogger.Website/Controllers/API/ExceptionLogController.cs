using ExceptionLogger.Core.Abstracts;
using ExceptionLogger.Core.Factories;
using ExceptionLogger.Core.Models;
using ExceptionLogger.Website.Factories;
using ExceptionLogger.Website.Hubs;
using ExceptionLogger.Website.Models;
using Microsoft.AspNet.SignalR;
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
            controller = ControllerFactory.CreateExceptionLogController("MONGOLAB_URI");
        }

        // GET api/values
        public IEnumerable<ExceptionLogDTO> Get()
        {
            try
            {
                var list = controller.Get().ToList();
                //var filteredList = new List<ExceptionLog>();
                List<ExceptionLogDTO> result = new List<ExceptionLogDTO>();

                #region FILTERS
                //DATE
                if (HttpContext.Current.Request.QueryString["date"] != null)
                {
                    DateTime date = DateTime.Now;
                    DateTime.TryParse(HttpContext.Current.Request.QueryString["date"].ToString(), out date);
                    list = list.Where(x =>
                            x.Date.Date == date.Date
                        ).ToList();
                }
                //MIN DATE
                if (HttpContext.Current.Request.QueryString["date"] == null && HttpContext.Current.Request.QueryString["mindate"] != null)
                {
                    DateTime date = DateTime.Now;
                    DateTime.TryParse(HttpContext.Current.Request.QueryString["mindate"], out date);
                    list = list.Where(x =>
                            x.Date.Date >= date.Date
                        ).ToList();
                }
                //MAXDATE
                if (HttpContext.Current.Request.QueryString["date"] == null && HttpContext.Current.Request.QueryString["maxdate"] != null)
                {
                    DateTime date = DateTime.Now;
                    DateTime.TryParse(HttpContext.Current.Request.QueryString["maxdate"], out date);
                    list = list.Where(x =>
                            x.Date.Date <= date.Date
                        ).ToList();
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
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        //GET api/values/5
        public ExceptionLogDTO Get(string id)
        {
            try
            {
                var exceptionLog = controller.Get(id);
                ExceptionLogDTO result = null;

                if (exceptionLog == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                result = ViewModelFactory.MapToExceptionLogVM(exceptionLog);
                return result;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]ExceptionLogDTO exception)
        {
            try
            {
                ExceptionLog log = new ExceptionLog();

                if(exception == null)
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);

                log.Date = DateTime.Now;
                log.Browser = exception.Browser;
                log.CID = exception.Browser;
                log.Exception = exception.Exception;
                log.IP = exception.IP;
                log.Is404Error = exception.Is404Error;
                log.Referer = exception.Referer;
                log.Server = exception.Server;
                log.StackTrace = exception.StackTrace;
                log.URL = exception.URL;
                //add to db
                controller.Add(log);
                exception.Id = log.Id.ToString();

                //SEND SIGNALR CLIENTS
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                hubContext.Clients.All.hello();

                var response = this.Request.CreateResponse<ExceptionLogDTO>(HttpStatusCode.Created, exception);
                string uri = String.Format("{0}{1}/api/ExceptionLog/{2}", HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter , HttpContext.Current.Request.Url.Host, exception.Id);
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
