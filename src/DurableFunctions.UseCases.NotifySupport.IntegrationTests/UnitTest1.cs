using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctions.TestHelpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace DurableFunctions.UseCases.NotifySupport.IntegrationTests
{
    public class UnitTest1
    {
        private readonly IConfigurationRoot _config;

        /// <summary>
        /// In order to use an Azure Storage Account: 
        ///     dotnet user-secrets set AzureWebJobsStorage "DefaultEndpointsProtocol=https;AccountName=###;AccountKey=###ß==;EndpointSuffix=core.windows.net"
        ///
        /// Otherwise the storage emulator is used as a fallback.
        ///
        /// Read more: https://github.com/riezebosch/AzureFunctions.TestHelpers
        /// </summary>
        public UnitTest1() =>
            _config = new ConfigurationBuilder()
                .AddUserSecrets<UnitTest1>()
                .Build();
        
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var connectionString = _config["AzureWebJobsStorage"] ?? "UseDevelopmentStorage=true";
            Environment.SetEnvironmentVariable("AzureWebJobsStorage", connectionString);
            Environment.SetEnvironmentVariable("AzureWebJobsSupportContactStorage", connectionString);
            Environment.SetEnvironmentVariable("SupportContactTableName", "SupportContacts");
            
            _ = typeof(NotifySupportHttpClient).FullName; // make sure the type is loaded to be resolved as function

            object response = null;
            using var host = new HostBuilder()
                .ConfigureWebJobs(builder => builder
                    .AddHttp(options => options.SetResponse = (r, o) => response = o)
                    .AddDurableTask(options => options.NotificationUrl = new Uri("https://asdf")) // req'd for CreateCheckStatusResponse
                    .AddAzureStorage()
                    .AddAzureStorageCoreServices())
                .Build();

            await host.StartAsync();
            var jobs = host.Services.GetService<IJobHost>();

            // Act
            var request = new DummyHttpRequest("{ 'Message': '', 'Severity': 1 }") { ContentType = "application/octet-stream", Method = "POST" };
            request.Headers["Content-Type"] = "application/json";
            
            await jobs.CallAsync(nameof(NotifySupportHttpClient), new Dictionary<string, object>
            {
                ["message"] = request
            });
            
            // Assert
            var message = Assert.IsType<HttpResponseMessage>(response);
            Assert.Equal(message.StatusCode, HttpStatusCode.Accepted);
        }
    }
}