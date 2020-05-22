namespace TaechIdeas.Core.Core.User.Dto
{
    public class LoginUserOut
    {
        //MUST BE AS IN STORED PROCEDURE
        public string ResultExecutionCode { get; set; }

        //MUST BE AS IN STORED PROCEDURE
        public string USPReturnValue { get; set; }

        //MUST BE AS IN STORED PROCEDURE
        public bool isError { get; set; }
    }
}