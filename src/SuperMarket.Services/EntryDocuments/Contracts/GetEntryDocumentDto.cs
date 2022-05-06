using System;

namespace SuperMarket.Services.EntryDocuments.Contracts
{
    public class GetEntryDocumentDto
    {
        public int id { get; set; }
        public int GoodsCount { get; set; }
        public int BuyPrice { get; set; }
        public DateTime DateBuy { get; set; }

        public int GoodsId { get; set; }
    }
}
