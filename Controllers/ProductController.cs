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

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> GetAsync()
    {
        var products = (await _productRepository.GetListAsync()).Select(product => product.AdaptDto());
        return products;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, UpdateProductDto updateProductDto)
    {
        var currentProduct = await _productRepository.GetAsync(id);
        if (currentProduct is null) return NotFound();

        currentProduct.Name = updateProductDto.Name;
        currentProduct.Description = updateProductDto.Description;
        currentProduct.Price = updateProductDto.Price;

        await _productRepository.UpdateAsync(currentProduct);

        return Ok(currentProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentProduct = await _productRepository.GetAsync(id);
        if (currentProduct is null) return NotFound();

        await _productRepository.RemoveAsync(id);

        return NoContent();
    }
}