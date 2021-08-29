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
            [ActivityTrigger] string bankAccount,
            ILogger logger)
        {
            await _customerDataService.GetCustomerAsync(bankAccount);
        }
    }
}