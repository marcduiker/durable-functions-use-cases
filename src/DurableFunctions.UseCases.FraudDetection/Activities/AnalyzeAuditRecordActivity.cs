namespace DurableFunctions.UseCases.FraudDetection.Activities
{
    public class AnalyzeAuditRecordActivity
    {
         private readonly ICustomerDataService _customerDataService;

         public GetCustomerActivity(ICustomerDataService customerDataService)
         {
             _customerDataService = customerDataService;
         }

        [FunctionName(nameof(AnalyzeAuditRecordActivity))]
        public async Task<Customer> Run(
            [ActivityTrigger] AuditRecord auditRecord,
            ILogger logger)
        {
            await _customerDataService.GetCustomerAsync(bankAccount);
        }
    }
}