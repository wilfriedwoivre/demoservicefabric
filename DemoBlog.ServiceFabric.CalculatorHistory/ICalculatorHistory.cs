using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorHistory.Model;
using Microsoft.ServiceFabric.Services;

namespace DemoBlog.ServiceFabric.CalculatorHistory
{
    public interface ICalculatorHistory : IService
    {
        Task PushHistory(Operation operation);

        Task<List<Operation>> GetAllOperations();
    }
}
