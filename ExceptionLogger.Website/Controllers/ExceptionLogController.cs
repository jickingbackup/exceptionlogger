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
            var list = controller.Get();
            List<ExceptionLogViewModel> result = new List<ExceptionLogViewModel>();

            foreach (var item in list)
            {
                var i = new ExceptionLogViewModel();

                i.CID = item.CID;
                i.Id = item.Id.ToString();

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
        public void Delete(int id)
        {
        }
    }
}
