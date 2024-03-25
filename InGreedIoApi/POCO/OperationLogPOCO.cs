using InGreedIoApi.Model;

namespace InGreedIoApi.POCO
{
    public class OperationLogPOCO
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }
        public ApiUserPOCO User { get; set; }
        public string Details { get; set; }
        public int OperationTypeId { get; set; }
        public OperationTypePOCO OperationType { get; set; }
    }
}