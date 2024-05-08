using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MerchStore.Models
{
	public class Product
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string productId { get; set; } // PK

		[BsonElement("productName")]
		public string productName { get; set; }

		[BsonElement("productDescription")]
		public string productDescription { get; set; }

		[BsonElement("productPrice")]
		public double productPrice { get; set; }

		[BsonElement("productStock")]
		public int productStock { get; set; }

		[BsonElement("categoryName")]
		public string categoryName { get; set; }
	}
}
