namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public class FakeCustomerDataService : ICustomerDataService
    {
        public Task<Customer> GetCustomerASync(string bankAccount)
        {
            // TODO: create a fake customer
            return Task.FromResult(new Customer());
        }
    }
}