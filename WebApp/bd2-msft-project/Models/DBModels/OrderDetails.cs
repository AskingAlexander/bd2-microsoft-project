namespace bd2_msft_project.Models.DBModels
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
