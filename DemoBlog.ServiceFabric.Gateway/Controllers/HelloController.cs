using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DemoBlog.ServiceFabric.Calculator;
using Microsoft.ServiceFabric.Services;
using Microsoft.ServiceFabric.Services.Wcf;

namespace DemoBlog.ServiceFabric.Gateway.Controllers
{
    [RoutePrefix("hello")]
    public class HelloController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Hello()
        {
            return Ok($"Hello world !");
        }
    }

}
