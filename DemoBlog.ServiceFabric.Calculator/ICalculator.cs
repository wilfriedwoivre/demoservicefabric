using System.ServiceModel;
using System.Threading.Tasks;

namespace DemoBlog.ServiceFabric.Calculator
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        Task<int> AddAsync(int a, int b);

        [OperationContract]
        Task<int> SubstractAsync(int a, int b);
    }
}