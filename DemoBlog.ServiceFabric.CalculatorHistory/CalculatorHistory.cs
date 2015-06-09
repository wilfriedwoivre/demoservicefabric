using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorHistory;
using CalculatorHistory.Model;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services;

namespace DemoBlog.ServiceFabric.CalculatorHistory
{
    public class CalculatorHistory : StatefulService, ICalculatorHistory
    {
        private const string HistoryListName = "calculhistory";
        protected override ICommunicationListener CreateCommunicationListener()
        {
            return new ServiceCommunicationListener<CalculatorHistory>(this);
        }

        public async Task PushHistory(Operation operation)
        {
            var operations = await this.StateManager.GetOrAddAsync<IReliableDictionary<DateTime, List<Operation>>>(HistoryListName);


            using (var tx = this.StateManager.CreateTransaction())
            {
                var currentDay = await operations.TryGetValueAsync(tx, DateTimeOffset.Now.Date);

                if (currentDay.HasValue)
                {
                    var current = currentDay.Value;

                    current.Add(operation);
                }
                else
                {
                    var current = new List<Operation> {operation};

                    await operations.TryAddAsync(tx, DateTimeOffset.Now.Date, current);
                }

                await tx.CommitAsync();
            }
        }

        public async Task<List<Operation>> GetAllOperations()
        {
            var operations = await this.StateManager.GetOrAddAsync<IReliableDictionary<DateTime, List<Operation>>>(HistoryListName);


            using (var tx = this.StateManager.CreateTransaction())
            {
                var currentDay = await operations.TryGetValueAsync(tx, DateTimeOffset.Now.Date);

                if (currentDay.HasValue)
                {
                    var current = currentDay.Value;

                    return current;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
