using MongoDB.Driver;
using SriBalajiBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriBalajiBilling.DataAccess
{
    internal class MongoDbUtil
    {

         private static MongoClient client;
         private static IMongoDatabase database;
        //static IMongoCollection<Product> collection;



        public MongoDbUtil() {
            //IMongoCollection<Product> collection = database.GetCollection<Product>("products");
            initialize();  
        }
        public static void initialize()
        {
            try
            {
              client = new MongoClient("mongodb://localhost:27017");
              database = client.GetDatabase("sribalaji");
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public  static IMongoDatabase getDataBase()
        {
            if(database == null )
            {
                initialize();
            }
            return database;
        }



    }
    
}
