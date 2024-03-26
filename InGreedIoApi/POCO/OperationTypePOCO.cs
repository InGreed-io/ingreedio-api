namespace InGreedIoApi.POCO
{
    public class OperationTypePOCO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<OperationLogPOCO> Operations { get; set; }
    }
}