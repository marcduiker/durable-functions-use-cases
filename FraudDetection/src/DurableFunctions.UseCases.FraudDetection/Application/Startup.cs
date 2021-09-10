using System;
using DurableFunctions.UseCases.FraudDetection.Application;
using DurableFunctions.UseCases.FraudDetection.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

[assembly: FunctionsStartup(typeof(Startup))]
namespace DurableFunctions.UseCases.FraudDetection.Application
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICustomerDataService, FakeCustomerDataService>();
            builder.Services.AddSingleton<AuthHandler>();
            builder.Services.AddRefitClient<IFraudAnalysisService>(new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(
                            new Newtonsoft.Json.JsonSerializerSettings
                            {
                                DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
                                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                            }
                        )
                })
                .AddHttpMessageHandler<AuthHandler>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.github.com"));
        }
    }
}