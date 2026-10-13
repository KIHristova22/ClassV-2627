namespace WebApplication3.DTOs
{
    public record ItemDto(int Id, string Name, string Description, decimal Price, DateTime CreatedAt);
    public record CreateItemDto(string Name, string Description, decimal Price);
}