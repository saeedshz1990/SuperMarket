namespace SuperMarket.Services.Goodses.Contracts
{
    public class AddGoodsDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int MinimumInventory { get; set; }
        public int SalesPrice { get; set; }
        public string UniqueCode { get; set; }
        public int CategoryId { get; set; }
        public int? SalesInvoiceId { get; set; }
        public int? EntryDocumentId { get; set; }
    }
}
