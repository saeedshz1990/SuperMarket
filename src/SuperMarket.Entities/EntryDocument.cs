using System;

namespace SuperMarket.Entities
{
    public class EntryDocument :EntityBase
    {
        public string Name { get; set; }
        public int BuyPrice { get; set; }
        public DateTime DateBuy { get; set; }

        public int GoodsId { get; set; }
        public Goods Goods { get; set; }

    }
}
