namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class WorkflowDispatchEvent
    {
        public WorkflowDispatchEvent(string recordId)
        {
            Ref = "main";
            Options = new { recordId = recordId };
        }

        public string Ref { get; set;}

        public object Options { get; set; }
    }
}