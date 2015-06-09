using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Services;
using Microsoft.ServiceFabric.Services.Wcf;

namespace DemoBlog.ServiceFabric.Calculator
{
    public class CalculatorStatelessService : StatelessService
    {
        protected override ICommunicationListener CreateCommunicationListener()
        {
            WcfCommunicationListener communicationListener = new WcfCommunicationListener(typeof(ICalculator), typeof(CalculatorService))
            {
                EndpointResourceName = "ServiceEndpoint",

                Binding = WcfCalculatorUtils.GetBinding()
            };

            return communicationListener;
        }
    }
}
