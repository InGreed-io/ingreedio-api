using InGreedIoApi.POCO;

namespace InGreedIoApi.DTO
{
    public record ReviewDTO(int Id, string Username, string Text, float Rating, string UserId);
}