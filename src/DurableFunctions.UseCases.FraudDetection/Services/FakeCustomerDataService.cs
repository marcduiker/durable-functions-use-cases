using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public class FakeCustomerDataService : ICustomerDataService
    {
        public Task<Customer> GetCustomerAsync(string bankAccount)
        {
            // TODO: create a fake customer
            return Task.FromResult(new Customer());
        }
    }
}