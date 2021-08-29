namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public interface ICustomerDataService
    {
        Task<Customer> GetCustomerASync(string bankAccount);
    }
}