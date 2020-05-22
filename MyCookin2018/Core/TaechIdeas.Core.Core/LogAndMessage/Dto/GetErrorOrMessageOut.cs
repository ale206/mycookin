namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class GetErrorOrMessageOut
    {
        public string ResultExecutionCode { get; set; }

        public string USPReturnValue { get; set; }

        public bool isError { get; set; }
    }
}