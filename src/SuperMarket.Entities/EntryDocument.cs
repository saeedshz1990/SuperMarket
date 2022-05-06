using System;
using System.Collections.Generic;

namespace SuperMarket.Entities
{
    public class EntryDocument :EntityBase
    {
        public EntryDocument()
        {
            Goods = new HashSet<Goods>();
        }

        public int GoodsCount{ get; set; }
        public int BuyPrice { get; set; }
        public DateTime DateBuy { get; set; }

        public int GoodsId { get; set; }
        public HashSet<Goods> Goods { get; set; }

    }
}
