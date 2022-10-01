namespace Commerce.Product.Service;

public record ProductDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

public record CreateProductDto(string Name, string Description, decimal Price);

public record UpdateProductDto(string Name, string Description, decimal Price);