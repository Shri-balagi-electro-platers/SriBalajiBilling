using MongoDB.Bson;
using MongoDB.Driver;
using SriBalajiBilling.DataAccess;
using SriBalajiBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriBalajiBilling.service
{

   
    public class BillService
    {
        IMongoDatabase database;
        ProductRepository productRepository;
        BillRepository billingRepository;
        public BillService()
        {
            database = MongoDbUtil.getDataBase();
            productRepository = new ProductRepository();
            billingRepository = new BillRepository();
        }

        public string GenerateBillNumber()
        {

            var lastBill = billingRepository.GetLastBillNo();

            if (lastBill != null)
            {
                // Extract the last bill number and increment it
                int lastBillNumber = int.Parse(lastBill.BillNo);
                int nextBillNumber = lastBillNumber + 1;

                // Format the next bill number as a 6-digit string with leading zeros
                string formattedNextBillNumber = nextBillNumber.ToString("D6");

                return formattedNextBillNumber;
            }
            else
            {
                // No bills exist yet, start with "000001"
                return "000001";
            }
        }

        public void saveBill()
        {

        }

        internal bool savebill(Bill bill)
        {
            return billingRepository.Save(bill);
        }
    }
}
