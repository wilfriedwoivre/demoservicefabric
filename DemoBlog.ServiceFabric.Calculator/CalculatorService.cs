using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DemoBlog.ServiceFabric.Calculator
{
    public class CalculatorService : ICalculator
    {
        public Task<int> AddAsync(int a, int b)
        {
            return Task.FromResult(a + b);
        }

        public Task<int> SubstractAsync(int a, int b)
        {
            return Task.FromResult(a - b);
        }
    }
}
