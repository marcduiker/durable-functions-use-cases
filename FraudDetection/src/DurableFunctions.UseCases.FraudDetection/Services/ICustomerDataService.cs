using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public interface ICustomerDataService
    {
        Task<Customer> GetCustomerAsync(string bankAccount);
    }
}