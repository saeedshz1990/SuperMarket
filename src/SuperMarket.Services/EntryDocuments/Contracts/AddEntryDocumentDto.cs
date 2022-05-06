using System;

namespace SuperMarket.Services.EntryDocuments.Contracts
{
    public class AddEntryDocumentDto
    {
        public int GoodsCount { get; set; }
        public int BuyPrice { get; set; }
        public DateTime DateBuy { get; set; }

        public int GoodsId { get; set; }
    }
}
