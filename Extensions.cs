namespace Commerce.Product.Service;

public static class Extensions
{
    public static ProductDto AdaptDto(this Entities.Product product) => new ProductDto(product.Id, product.Name, product.Description, product.Price, product.CreatedDate);

}