﻿using System;

namespace SuperMarket.Services.SalesInvoices.Contracts
{
    public class UpdateSalesInvoiceDto
    {
        public string CustomerName { get; set; }
        public int Count { get; set; }
        public int SalesPrice { get; set; }
        public DateTime SalesDate { get; set; }

        public int GoodsId { get; set; }
    }
}
