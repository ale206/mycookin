using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class GetPicturesToCheckInput : TokenRequiredInput
    {
        public int NumberOfResults { get; set; }
    }
}