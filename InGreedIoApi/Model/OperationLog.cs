namespace InGreedIoApi.Model
{
    public class OperationLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public ApiUser User { get; set; }
        public string Details { get; set; }
        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }
    }
}