using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriBalajiBilling.Models
{
    public class Bill
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string BillNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string GSTIN { get; set; }
        public string CustomerPan { get; set; }
        public string CustomerState { get; set; }
        public string PlaceOfSupply { get; set; }
        public List<BillItem> Items { get; set; } // List of items in the bill
        public double TotalAmount { get; set; }
        public double Discount { get; set; }
        public double NetPay { get; set; }
    }

    public class BillItem
    {
        public int SerialNo { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double SGST { get; set; }
        public double CGST { get; set; }
        public double TaxAmount { get; set; }
        public double ProductAmount { get; set; }
    }



}
