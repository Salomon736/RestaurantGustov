namespace Restaurant.Domain.Responses;

public record Pager<T>(
    List<T> Items,
    int TotalItems,
    int PageIndex,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}