using System;
using System.Collections.Generic;

namespace SuperMarket.Entities
{
    public class SalesInvoice :EntityBase
    {
        public SalesInvoice()
        {
            Goods = new HashSet<Goods>();
        }
        public string CustomerName { get; set; }
        public int Count { get; set; }
        public int SalesPrice { get; set; }
        public DateTime SalesDate { get; set; }
        
        public int GoodsId { get; set; }
        public HashSet<Goods> Goods { get; set; }
        
        
        
    }
}
