using System;

namespace bd2_msft_project.Models.DBModels
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime DateAdded { get; set; }
        public int RemainingStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
