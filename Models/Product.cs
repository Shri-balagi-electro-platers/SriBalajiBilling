using Microsoft.Build.Framework;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SriBalajiBilling.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId productId { get; set; }
        //[Required(ErrorMessage = "Product name is required.")]
        public string name { get; set; }    
        public double price { get; set; }   
        public string description { get; set; }
        
        public string ToProductString()
        {
            return $"Product: [productId:{productId}, Name: {name}, Price: {price}, Description:{description}";
        }
    }
}
