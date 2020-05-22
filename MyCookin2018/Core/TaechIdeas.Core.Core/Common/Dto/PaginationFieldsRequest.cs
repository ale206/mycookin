namespace TaechIdeas.Core.Core.Common.Dto
{
    public class PaginationFieldsRequest
    {
        public int Offset { get; set; }
        public int Count { get; set; }
        public string OrderBy { get; set; }
        public bool IsAscendant { get; set; }
        public string Search { get; set; }
    }
}