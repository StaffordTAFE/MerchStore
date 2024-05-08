using MerchStore.Models;
using MerchStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace MerchStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly MerchService _merchService;

	public ProductsController(MerchService MerchService) =>
		_merchService = MerchService;

	[HttpGet]
	public async Task<List<Product>> Get() =>
		await _merchService.GetAsync();

	[HttpGet("{productId:length(24)}")]
	public async Task<ActionResult<Product>> Get(string productId)
	{
		var product = await _merchService.GetAsync(productId);

		if (product is null)
		{
			return NotFound();
		}

		return product;
	}

	[HttpPost]
	public async Task<IActionResult> Post(Product newProduct)
	{
		await _merchService.CreateAsync(newProduct);

		return CreatedAtAction(nameof(Get), new { productId = newProduct.productId }, newProduct);
	}

	[HttpPut("{productId:length(24)}")]
	public async Task<IActionResult> Update(string productId, Product updatedProduct)
	{
		var product = await _merchService.GetAsync(productId);

		if (product is null)
		{
			return NotFound();
		}

		updatedProduct.productId = product.productId;

		await _merchService.UpdateAsync(productId, updatedProduct);

		return NoContent();
	}

	[HttpDelete("{productId:length(24)}")]
	public async Task<IActionResult> Delete(string productId)
	{
		var product = await _merchService.GetAsync(productId);

		if (product is null)
		{
			return NotFound();
		}

		await _merchService.RemoveAsync(productId);

		return NoContent();
	}
}