namespace SuperMarket.Services.Goodses.Contracts
{
    public class GetGoodsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int MinimumInventory { get; set; }
        public int SalesPrice { get; set; }
        public string UniqueCode { get; set; }
        public int CategoryId { get; set; }
    }
}
