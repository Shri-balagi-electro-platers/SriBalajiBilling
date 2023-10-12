using MongoDB.Bson;
using MongoDB.Driver;
using SriBalajiBilling.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriBalajiBilling.DataAccess
{
    public class ProductRepository
    {
        //private MongoDbUtil mongoDbUtil = new MongoDbUtil();
        private static IMongoDatabase mongoDatabase;
        private static IMongoCollection<Product> productCollection;

        public ProductRepository()
        {

        }

        public void init()
        {
         mongoDatabase = MongoDbUtil.getDataBase();
         productCollection = mongoDatabase.GetCollection<Product>("products");

        }
        public bool add(Product product)
        {
            if(productCollection==null)
                init();
            try
            {
                 productCollection.InsertOne(product);
                Console.WriteLine("Inserted Successfully.");
                return true;
            }
            catch(Exception e) { 
                Console.WriteLine(e);
                return false;
            }
            return false;
        }


        public bool update(Product product,string productId)
        {
            if (productCollection == null)
                init();
            var updateProductData = Builders<Product>.Update
                .Set(p => p.name, product.name)
                .Set(p => p.description, product.description)
                .Set(p => p.price, product.price);
            try
            {
                productCollection.UpdateOne(p => p.productId == ObjectId.Parse(productId),updateProductData);
                
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public List<Product> ProductByKeyword(string keyword)
        {
            List<Product> results=null;
            try
            {
                if (productCollection == null)
                    init();

                // Define a filter to perform the search
                var filter = Builders<Product>.Filter.Regex("name", new BsonRegularExpression(keyword, "i")); 
                Console.WriteLine(filter.ToString());
                Console.WriteLine(productCollection.ToString());
            // Find documents matching the filter
            results =  productCollection.Find(filter).ToList();
            Console.WriteLine($"debug:ProductRepository:ProductByKeyword:results:{results}");
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return results;
        }

        public Product FindByProductName(string productName)
        {
            if (productCollection == null)
                init();

            // Define a filter to find the product by its name
            var filter = Builders<Product>.Filter.Eq(p => p.name, productName);

            // Use the filter to find the product
            Product foundProduct = productCollection.Find(filter).FirstOrDefault();

            return foundProduct;
        }
    }
}
