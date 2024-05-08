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
		await _ProductCollection.Find(_ => true).ToListAsync();

	// get by id
	public async Task<Product?> GetAsync(string id) =>
		await _ProductCollection.Find(x => x.productId == id).FirstOrDefaultAsync();

	// add a product
	public async Task CreateAsync(Product newMerch) =>
		await _ProductCollection.InsertOneAsync(newMerch);

	// update a product by id
	public async Task UpdateAsync(string id, Product updatedMerch) =>
		await _ProductCollection.ReplaceOneAsync(x => x.productId == id, updatedMerch);

	// remove a product by id
	public async Task RemoveAsync(string id) =>
		await _ProductCollection.DeleteOneAsync(x => x.productId == id);
}