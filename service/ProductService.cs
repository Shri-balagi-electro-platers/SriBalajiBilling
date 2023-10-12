using MongoDB.Driver;
using SriBalajiBilling.DataAccess;
using SriBalajiBilling.Forms;
using SriBalajiBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriBalajiBilling.service
{
    internal class ProductService
    {
         IMongoDatabase database;
         IMongoCollection<Product> productCollections;

         ProductRepository productRepository;

        public ProductService()
        {
            database =  MongoDbUtil.getDataBase();
            productRepository = new ProductRepository();
        }
        public List<Product> GetAllProducts()
        {
            productCollections = database.GetCollection<Product>("products");
            List<Product> products = productCollections.AsQueryable().ToList<Product>();
            return products;
        }
        public List<string> GetAllProductNames()
        {
            var productNames = GetAllProducts().Select(p => p.name).ToList();
            return productNames;
        }

        public Product GetProductByName(string productName)
        {
            return productRepository.FindByProductName(productName);
        }



        public List<Product> getProductByKeyword(string keyword)
        {
            var list = productRepository.ProductByKeyword(keyword);
            return list;
        }

        public bool addProduct(Product product)
        {
            return productRepository.add(product);
        }
        public bool updateProduct(Product product,string productId)
        {
            return productRepository.update(product,productId);
        }

    }

}
