using System.Threading.Tasks;
using DurableFunctions.UseCases.Builders;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public class FakeCustomerDataService : ICustomerDataService
    {
        public Task<Customer> GetCustomerAsync(string bankAccount)
        {
            return Task.FromResult(FakeCustomerBuilder.Create(bankAccount));
        }
    }
}