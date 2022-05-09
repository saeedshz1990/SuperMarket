using System;

namespace SuperMarket.Entities
{
    public class EntryDocument : EntityBase
    {
        public int GoodsCount { get; set; }
        public int BuyPrice { get; set; }
        public DateTime DateBuy { get; set; }

        public int GoodsId { get; set; }
        public Goods Goods { get; set; }
    }
}
