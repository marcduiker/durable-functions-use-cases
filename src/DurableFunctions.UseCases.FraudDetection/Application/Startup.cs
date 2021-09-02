using System;
using DurableFunctions.UseCases.FraudDetection.Application;
using DurableFunctions.UseCases.FraudDetection.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

[assembly: FunctionsStartup(typeof(Startup))]
namespace DurableFunctions.UseCases.FraudDetection.Application
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICustomerDataService, FakeCustomerDataService>();
            builder.Services.AddSingleton<IGitHubClient>(
                s => new GitHubClient(
                    new ProductHeaderValue("FraudDetectionDemo")) 
                    { 
                        Credentials = new Credentials(Environment.GetEnvironmentVariable("GitHubWebhookPAT")) 
                    }
                );
        }
    }
}