using API.Helpers;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse respone, PaginationHeader header )
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            respone.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));
            respone.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
