using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class GetPicturesToCheckRequest
    {
        public int NumberOfResults { get; set; }
        public CheckTokenRequest CheckTokenRequest { get; set; }
    }
}