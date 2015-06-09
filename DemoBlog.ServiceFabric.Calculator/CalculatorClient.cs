using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services;
using Microsoft.ServiceFabric.Services.Wcf;

namespace DemoBlog.ServiceFabric.Calculator
{
    public class CalculatorClient : ServicePartitionClient<WcfCommunicationClient<ICalculator>>, ICalculator
    {
        public CalculatorClient(
            WcfCommunicationClientFactory<ICalculator> clientFactory,
            Uri serviceName)
            : base(clientFactory, serviceName)
        {
        }

        public static CalculatorClient GetClient()
        {
            return new CalculatorClient(new WcfCommunicationClientFactory<ICalculator>
                (new ServicePartitionResolver(() => new FabricClient()),
                    WcfCalculatorUtils.GetBinding()), 
                    WcfCalculatorUtils.ServiceUri);
        }

        public Task<int> AddAsync(int a, int b)
        {
            return this.InvokeWithRetryAsync(
                client => client.Channel.AddAsync(a, b));
        }

        public Task<int> SubstractAsync(int a, int b)
        {
            return this.InvokeWithRetryAsync(
                client => client.Channel.SubstractAsync(a, b));
        }
    }
}
