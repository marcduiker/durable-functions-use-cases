using Bogus;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Builders
{
    public static class FakeTransactionBuilder
    {
        public static Transaction Create()
        {
            var fakeTransaction = new Faker<Transaction>("en_US")
                .RuleFor( t => t.Amount, f => f.Finance.Amount())
                .RuleFor( t => t.Currency, f => f.Finance.Currency().Code)
                .RuleFor( t => t.CreditorBankAccount, f => f.Finance.Iban())
                .RuleFor( t => t.DebtorBankAccount, f => f.Finance.Iban())
                .RuleFor( t => t.TimeStampUtc, f => f.Date.Recent())
                .Generate();
            
            return fakeTransaction;
        }
    }
}