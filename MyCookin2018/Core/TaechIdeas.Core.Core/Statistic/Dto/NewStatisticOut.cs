namespace TaechIdeas.Core.Core.Statistic.Dto
{
    public class NewStatisticOut
    {
        //MUST BE AS IN STORED PROCEDURE
        public string ResultExecutionCode { get; set; }

        //MUST BE AS IN STORED PROCEDURE
        public string USPReturnValue { get; set; }

        //MUST BE AS IN STORED PROCEDURE
        public bool isError { get; set; }
    }
}