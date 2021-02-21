using System.Collections.Generic;
using DurableFunctions.UseCases.NotifySupport;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctions.UseCases
{
    public class GetSupportContactActivity
    {
        [FunctionName(nameof(GetSupportContactActivity))]
        public IEnumerable<SupportContactEntity> Run(
            [ActivityTrigger] string team,
            [Table("%SupportContactTableName%")] CloudTable cloudTable)
        {
             var teamFilter = new TableQuery<SupportContactEntity>()
                .Where(
                    TableQuery.GenerateFilterCondition(
                        nameof(SupportContactEntity.PartitionKey), 
                        QueryComparisons.Equal,
                        team))
                .OrderBy(nameof(SupportContactEntity.Order));
            var supportContacts = cloudTable.ExecuteQuery<SupportContactEntity>(teamFilter);

            return supportContacts;
        }
    }
}