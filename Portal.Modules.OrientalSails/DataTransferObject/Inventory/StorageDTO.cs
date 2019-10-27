namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class StorageDTO
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string NameTree { get; set; }
    }
}