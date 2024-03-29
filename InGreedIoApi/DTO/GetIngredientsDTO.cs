namespace InGreedIoApi.DTO
{
    public class GetIngredientsQuery
    {
        public string Query { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}