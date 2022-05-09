using System;

namespace SuperMarket.Entities
{
    public class SalesInvoice : EntityBase
    {
        public string CustomerName { get; set; }
        public int Count { get; set; }
        public int SalesPrice { get; set; }
        public DateTime SalesDate { get; set; }

        public int GoodsId { get; set; }
        public Goods Goods { get; set; }
    }
}
