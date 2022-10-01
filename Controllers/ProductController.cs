using Commerce.Core;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Product.Service.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Entities.Product> _productRepository;

    public ProductController(IRepository<Entities.Product> productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
    {
        var product = (await _productRepository.GetAsync(id)).AdaptDto();

        if (product is null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostAsync(CreateProductDto createProductDto)
    {
        var newProduct = new Entities.Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _productRepository.CreateAsync(newProduct);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newProduct.Id }, newProduct);
    }
}