using Bogus;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Builders
{
    public static class FakeCustomerBuilder
    {
        public static Customer Create(string bankAccount)
        {
            var fakeCustomer = new Faker<Customer>("en_US")
                .RuleFor(c => c.BankAccount, bankAccount)
                .RuleFor(c => c.CountryCode, f => f.Address.CountryCode())
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .Generate();

            return fakeCustomer;
        }
    }
}