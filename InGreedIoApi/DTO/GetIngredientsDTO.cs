using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.DTO
{
    public record GetIngredientsQuery(string Query, int Page, int Limit);
}