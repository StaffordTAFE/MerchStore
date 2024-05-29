using MerchStore.Models;
using MerchStore.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MerchStore.Controllers;
#region Version 1
[ApiVersion("1.0")]
[Route("v1/products")]
public class ProductsControllerV1 : ControllerBase
{
	private readonly MerchService _merchService;

	public ProductsControllerV1(MerchService MerchService) =>
		_merchService = MerchService;

	//Get no filter
	[HttpGet]
	public async Task<ActionResult<List<Product>>> Get() =>
		await _merchService.GetAsync();

	//Get id
	[HttpGet("Id/{productId}")]
	public async Task<ActionResult<Product>> GetId(int productId)
	{
		var product = await _merchService.GetIdAsync(productId);

        if (product == null)
        {
            return NotFound();
        }

        return product;
	}

    //Get name
    [HttpGet("Search/{productName}")]
    public async Task<ActionResult<List<Product?>>> GetName(string productName)
    {
        List<Product> products = await _merchService.GetNameAsync(productName);

        if (products.Count == 0)
        {
            return NotFound();
        }

        return products;
    }

    //Get category
    [HttpGet("Category/{categoryName}")]
    public async Task<ActionResult<List<Product?>>> GetCategory(string categoryName)
    {
        List<Product> products = await _merchService.GetNameAsync(categoryName);

        if (products.Count == 0)
        {
            return NotFound();
        }

        return products;
    }

    //Get name
    [HttpGet("Price/{priceLower}-{priceUpper}")]
    public async Task<ActionResult<List<Product?>>> GetPrice(double priceLower, double priceUpper)
    {
        List<Product> products = await _merchService.GetPriceAsync(priceLower, priceUpper);

		if (products.Count == 0)
		{
			return NotFound();
		}

        return products;
    }

    [HttpPost]
	public async Task<IActionResult> Post(Product newProduct)
	{
		await _merchService.CreateAsync(newProduct);

		return CreatedAtAction(nameof(Get), new { productId = newProduct.productId }, newProduct);
	}

	[HttpPut("{productId:length(24)}")]
	public async Task<IActionResult> Update(int productId, Product updatedProduct)
	{
		var product = await _merchService.GetIdAsync(productId);

		if (product is null)
		{
			return NotFound();
		}

		updatedProduct.productId = product.productId;

		await _merchService.UpdateAsync(productId, updatedProduct);

		return NoContent();
	}

	[HttpDelete("{productId:length(24)}")]
	public async Task<IActionResult> Delete(int productId)
	{
		var product = await _merchService.GetIdAsync(productId);

		if (product is null)
		{
			return NotFound();
		}

		await _merchService.RemoveAsync(productId);

		return NoContent();
	}
}
#endregion

#region Version 2
[ApiVersion("2.0")]
[Route("v2/products")]
public class ProductsControllerV2 : ControllerBase
{
    private readonly MerchService _merchService;

    public ProductsControllerV2(MerchService MerchService) =>
        _merchService = MerchService;

    //Get no filter
    [HttpGet]
    public async Task<ActionResult<List<Product>>> Get() =>
        await _merchService.GetAsync();

    //Get id
    [HttpGet("Id/{productId}")]
    public async Task<ActionResult<Product>> GetId(int productId)
    {
        var product = await _merchService.GetIdAsync(productId);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    //Get name
    [HttpGet("Search/{productName}")]
    public async Task<ActionResult<List<Product?>>> GetName(string productName)
    {
        List<Product> products = await _merchService.GetNameAsync(productName);

        if (products.Count == 0)
        {
            return NotFound();
        }

        return products;
    }

    //Get category
    [HttpGet("Category/{categoryName}")]
    public async Task<ActionResult<List<Product?>>> GetCategory(string categoryName)
    {
        List<Product> products = await _merchService.GetNameAsync(categoryName);

        if (products.Count == 0)
        {
            return NotFound();
        }

        return products;
    }

    //Get name
    [HttpGet("Price/{priceLower}-{priceUpper}")]
    public async Task<ActionResult<List<Product?>>> GetPrice(double priceLower, double priceUpper)
    {
        List<Product> products = await _merchService.GetPriceAsync(priceLower, priceUpper);

        if (products.Count == 0)
        {
            return NotFound();
        }

        return products;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product newProduct)
    {
        await _merchService.CreateAsync(newProduct);

        return CreatedAtAction(nameof(Get), new { productId = newProduct.productId }, newProduct);
    }

    [HttpPut("{productId:length(24)}")]
    public async Task<IActionResult> Update(int productId, Product updatedProduct)
    {
        var product = await _merchService.GetIdAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        updatedProduct.productId = product.productId;

        await _merchService.UpdateAsync(productId, updatedProduct);

        return NoContent();
    }

    [HttpDelete("{productId:length(24)}")]
    public async Task<IActionResult> Delete(int productId)
    {
        var product = await _merchService.GetIdAsync(productId);

        if (product is null)
        {
            return NotFound();
        }

        await _merchService.RemoveAsync(productId);

        return NoContent();
    }
}
#endregion