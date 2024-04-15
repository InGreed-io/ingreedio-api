namespace InGreedIoApi.Model
{
    public class OperationType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<OperationType> Operations { get; set; }
    }
}