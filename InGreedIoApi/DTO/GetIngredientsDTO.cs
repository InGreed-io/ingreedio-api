using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.DTO
{
    public record GetIngredientsQuery
    {
        [FromQuery(Name = "Query")]
        public string Query { get; init; }

        [FromQuery(Name = "Page")]
        public int Page { get; init; }

        [FromQuery(Name = "Limit")]
        public int Limit { get; init; }
    }
}