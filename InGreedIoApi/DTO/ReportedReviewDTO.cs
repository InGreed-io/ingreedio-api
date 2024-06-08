namespace InGreedIoApi.DTO
{
    public record ReportedReviewDTO(int Id, string Username, string Text, float Rating, string UserId, int ReportsCount);
}
