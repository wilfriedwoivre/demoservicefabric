using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.Fabric.Query;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CalculatorHistory;
using Microsoft.ServiceFabric.Services;

namespace DemoBlog.ServiceFabric.CalculatorHistory
{
    public class CalculatorHistoricClient
    {
        private static readonly Uri serviceUri = new Uri("fabric:/DemoBlog.ServiceFabric.Application/DemoBlogServiceFabricCalculatorHistory");
        public static ICalculatorHistory GetClient()
        {
            var t = GetPartition();

            var partition = t.Result;

            Int64RangePartitionInformation partitionInfo = partition.PartitionInformation as Int64RangePartitionInformation;

            if (partitionInfo != null)
            {
                return ServiceProxy.Create<ICalculatorHistory>(partitionInfo.LowKey, serviceUri);
            }
            else
            {
                return null;
            }
        }

        public static async Task<Partition> GetPartition()
        {
            var fabricClient = new FabricClient();
            ServicePartitionList partitionList = await fabricClient.QueryManager.GetPartitionListAsync(serviceUri);

            return partitionList.First();
        }

        private static async Task<string> CreateNewRessource()
        {
            string newName = Guid.NewGuid().ToString();
            FabricClient client= new FabricClient();
            await client.ServiceManager.CreateServiceAsync(new StatefulServiceDescription()
            {
                ApplicationName = new Uri("fabric:/DemoBlog.ServiceFabric.Application"),
                ServiceName = new Uri(string.Concat(serviceUri.ToString(), "/", newName)),
                ServiceTypeName = "DemoBlogServiceFabricCalculatorHistoryType", 
                PartitionSchemeDescription = new UniformInt64RangePartitionSchemeDescription(),
                HasPersistedState = true,
                MinReplicaSetSize = 2,
                TargetReplicaSetSize = 3
            });

            return newName;
        } 
    }
}
