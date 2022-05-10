using System;

namespace SuperMarket.Services.SalesInvoices.Contracts
{
    public class GetSalesInvoiceDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int Count { get; set; }
        public int SalesPrice { get; set; }
        public DateTime SalesDate { get; set; }
        public int GoodsId { get; set; }
    }
}
