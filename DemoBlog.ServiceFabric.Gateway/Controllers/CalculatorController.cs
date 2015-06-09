using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CalculatorHistory;
using CalculatorHistory.Model;
using DemoBlog.ServiceFabric.Calculator;
using DemoBlog.ServiceFabric.CalculatorHistory;
using Microsoft.ServiceFabric.Services;

namespace DemoBlog.ServiceFabric.Gateway.Controllers
{
    [RoutePrefix("calculator")]
    public class CalculatorController : ApiController
    {
        private readonly CalculatorClient calculatorClient;
        private readonly ICalculatorHistory historic;

        public CalculatorController(CalculatorClient calculatorClient, ICalculatorHistory historic)
        {
            this.calculatorClient = calculatorClient;
            this.historic = historic;
        }

        [HttpGet]
        [Route("add")]
        public async Task<IHttpActionResult> Add(int a, int b)
        {
            var result = await this.calculatorClient.AddAsync(a, b);
            await this.historic.PushHistory(new Operation() {LeftValue = a, Operand = Operand.Add, RightValue = b});
            return Ok(result);
        }

        [HttpGet]
        [Route("substract")]
        public async Task<IHttpActionResult> Substract(int a, int b)
        {
            var result = await this.calculatorClient.SubstractAsync(a, b);
            await this.historic.PushHistory(new Operation() { LeftValue = a, Operand = Operand.Substract, RightValue = b });
            return Ok(result);
        }

        [HttpGet]
        [Route("historic")]
        public async Task<IHttpActionResult> Historic()
        {
            var content = await historic.GetAllOperations();
            return Ok(content);
        }
    }
}
