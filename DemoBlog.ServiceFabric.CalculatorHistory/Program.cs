using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using CalculatorHistory;

namespace DemoBlog.ServiceFabric.CalculatorHistory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (FabricRuntime fabricRuntime = FabricRuntime.Create())
                {
                    // This is the name of the ServiceType that is registered with FabricRuntime. 
                    // This name must match the name defined in the ServiceManifest. If you change
                    // this name, please change the name of the ServiceType in the ServiceManifest.
                    fabricRuntime.RegisterServiceType("DemoBlogServiceFabricCalculatorHistoryType", typeof(DemoBlog.ServiceFabric.CalculatorHistory.CalculatorHistory));

                    ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(DemoBlog.ServiceFabric.CalculatorHistory.CalculatorHistory).Name);

                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e);
                throw;
            }
        }
    }
}
