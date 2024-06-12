using MerchStore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MerchStore.Services;

public class MerchService
{
	private readonly IMongoCollection<Product> _ProductCollection;

	public MerchService(
		IOptions<MerchStoreDatabaseSettings> MerchStoreDatabaseSettings)
	{
		var mongoClient = new MongoClient(
			MerchStoreDatabaseSettings.Value.ConnectionString);

		var mongoDatabase = mongoClient.GetDatabase(
			MerchStoreDatabaseSettings.Value.DatabaseName);

		_ProductCollection = mongoDatabase.GetCollection<Product>(
			MerchStoreDatabaseSettings.Value.CollectionName);
	}

	// get all products
	public async Task<List<Product>> GetAsync() =>
		await _ProductCollection.Find(_ => true).SortBy(product => product.productName).ToListAsync();

    // get all products queryable
    public async Task<IQueryable<Product>> GetAsyncQueryable()
    {
        var productList = await _ProductCollection.Find(_ => true).SortBy(product => product.productName).ToListAsync();
        return (productList.AsQueryable());
    }

    //check if id exists
    public async Task<bool> DoesIdExistAsync(int id)
    {
        var product = await _ProductCollection.Find(x => x.productId == id).FirstOrDefaultAsync();
        return product != null;
    }


    // get by id
    public async Task<Product?> GetIdAsync(int id) =>
		await _ProductCollection.Find(x => x.productId == id).FirstOrDefaultAsync();

    // get by name
    public async Task<List<Product>> GetNameAsync(string name) =>
        await _ProductCollection.Find(x => x.productName.ToLower().Contains(name.ToLower())).ToListAsync();

	// get by category
    public async Task<List<Product>> GetCategoryAsync(string category) =>
    await _ProductCollection.Find(x => x.categoryName.ToLower() == category.ToLower()).ToListAsync();

    // get by price
    public async Task<List<Product>> GetPriceAsync(double priceLower, double priceUpper) =>
        await _ProductCollection.Find(x => x.productPrice < priceUpper && x.productPrice > priceLower).ToListAsync();

    // add a product
    public async Task CreateAsync(Product newMerch) =>
		await _ProductCollection.InsertOneAsync(newMerch);

	// update a product by id
	public async Task UpdateAsync(int id, Product updatedMerch) =>
		await _ProductCollection.ReplaceOneAsync(x => x.productId == id, updatedMerch);

	// remove a product by id
	public async Task RemoveAsync(int id) =>
		await _ProductCollection.DeleteOneAsync(x => x.productId == id);
}