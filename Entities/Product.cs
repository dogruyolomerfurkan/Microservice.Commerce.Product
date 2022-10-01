using Commerce.Core;

namespace Commerce.Product.Service.Entities;

public class Product : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}