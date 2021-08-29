using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;
using DurableFunctions.UseCases.FraudDetection.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctions.UseCases.FraudDetection.Activities
{
    public class GetCustomerActivity
    {
         private readonly ICustomerDataService _customerDataService;

         public GetCustomerActivity(ICustomerDataService customerDataService)
         {
             _customerDataService = customerDataService;
         }

        [FunctionName(nameof(GetCustomerActivity))]
        public async Task<Customer> Run(
            [ActivityTrigger] string bankAccount)
        {
            await _customerDataService.GetCustomerAsync(bankAccount);
        }
    }
}