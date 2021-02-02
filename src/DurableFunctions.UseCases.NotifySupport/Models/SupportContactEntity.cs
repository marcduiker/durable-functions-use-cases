using Microsoft.Azure.Cosmos.Table;

namespace DurableFunctions.UseCases.NotifySupport
{
    public class SupportContactEntity : TableEntity
    {
        public SupportContactEntity()
        {
        }

        public SupportContactEntity(string team, string name, string phoneNumber, int order)
        {
            PartitionKey = team;
            Team = team;
            Name = name;
            RowKey = name;
            PhoneNumber = phoneNumber;
            Order = order;
        }

        public string Name { get; set; }
        public int Order { get; set; }
        public string PhoneNumber { get; set; }
        public string Team { get; set; }
    }
}