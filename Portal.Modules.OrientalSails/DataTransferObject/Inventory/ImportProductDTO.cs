namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class ImportProductDTO
    {
        public int ClientImportId { get; set; }

        public int ImportId { get; set; }
        public int ImportProductId { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public int Quantity { get; set; }

        public virtual string QuantityUnit
        {
            get { return Quantity + " " + UnitName; }
        }

        public int StorageId { get; set; }
        public double Total { get; set; }
        public double UnitPrice { get; set; }
        public int UnitId { get; set; }
        public virtual string UnitName { get; set; }
    }
}
