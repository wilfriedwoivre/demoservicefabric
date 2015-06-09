using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services;

namespace DemoBlog.Common
{
    public class WcfClientCommunicationFactory : ClientCommunicationFactoryBase
    {
        public WcfCommunicationClientFactory(
            ServicePartitionResolver servicePartitionResolver = null,
            Binding binding = null,
            object callback = null,
            IList<IExceptionHandler> exceptionHandlers = null,
            IEnumerable<Type> doNotRetryExceptionTypes = null)
        {
            
        }
    }
}
