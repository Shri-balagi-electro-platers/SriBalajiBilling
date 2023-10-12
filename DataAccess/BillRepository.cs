using MongoDB.Bson;
using MongoDB.Driver;
using SriBalajiBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriBalajiBilling.DataAccess
{
    public class BillRepository { 

        private static IMongoDatabase mongoDatabase;
        private static IMongoCollection<Bill> billCollection;

        public BillRepository()
        {
            Init();
        }

        public void Init()
        {
            mongoDatabase = MongoDbUtil.getDataBase();
            billCollection = mongoDatabase.GetCollection<Bill>("bill");
        }

        public Bill GetLastBillNo()
        {
            var lastBill = billCollection.Find(new BsonDocument())
            .Sort(Builders<Bill>.Sort.Descending("billNo"))
            .Limit(1)
            .FirstOrDefault();
            return lastBill;

        }

        public bool Save(Bill bill)
        {
            try
            {
                Console.WriteLine($"debug: save(): {bill}");
                billCollection.InsertOne(bill);
                return true;
            }catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return false;
            }


            return false;
        }

    }
}
