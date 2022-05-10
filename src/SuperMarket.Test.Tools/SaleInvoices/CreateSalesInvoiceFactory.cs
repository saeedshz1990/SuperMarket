using System;
using SuperMarket.Entities;
using SuperMarket.Services.SalesInvoices.Contracts;

namespace SuperMarket.Test.Tools.SaleInvoices
{
    public static class CreateSalesInvoiceFactory
    {
        public static SalesInvoice CreateSalesInvoiceDto(string name, int count, int salesPrice,
            DateTime salesDate, int goodsId)
        {
            return new SalesInvoice
            {
                CustomerName = name,
                Count = count,
                SalesPrice = salesPrice,
                SalesDate = salesDate.Date,
                GoodsId = goodsId
            };
        }

        public static SalesInvoice CreateSalesInvoice(int goodsId)
        {
            return new SalesInvoice
            {
                CustomerName = "Saeed Ansari",
                Count = 12,
                SalesPrice = 2000,
                SalesDate = DateTime.Now.Date,
                GoodsId = goodsId
            };
        }

        public static AddSalesInvoiceDto CreateAddSalesInvoiceDto(string name, int count, int salesPrice,
            DateTime salesDate, int goodsId)
        {
            return new AddSalesInvoiceDto
            {
                CustomerName = name,
                Count = count,
                SalesPrice = salesPrice,
                SalesDate = salesDate.Date,
                GoodsId = goodsId
            };
        }

        public static AddSalesInvoiceDto CreateAddSalesInvoice(int goodsId)
        {
            return new AddSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                Count = 3,
                SalesPrice = 2000,
                SalesDate = DateTime.Now.Date,
                GoodsId = goodsId
            };
        }

        public static UpdateSalesInvoiceDto CreateUpdateSalesInvoiceDto(string name, int count, int salesPrice,
            DateTime salesDate, int goodsId)
        {
            return new UpdateSalesInvoiceDto
            {
                CustomerName = name,
                Count = count,
                SalesPrice = salesPrice,
                SalesDate = salesDate.Date,
                GoodsId = goodsId
            };
        }

        public static UpdateSalesInvoiceDto CreateUpdateSalesInvoice(int goodsId)
        {
            return new UpdateSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                Count = 5,
                SalesPrice = 2000,
                SalesDate = DateTime.Now.Date,
                GoodsId = goodsId
            };
        }
        public static GetSalesInvoiceDto CreateGetSalesInvoiceDto(int id,string name, int count, int salesPrice,
            DateTime salesDate, int goodsId)
        {
            return new GetSalesInvoiceDto
            {
                Id = id,
                CustomerName = name,
                Count = count,
                SalesPrice = salesPrice,
                SalesDate = salesDate.Date,
                GoodsId = goodsId
            };
        }

        public static GetSalesInvoiceDto CreateGetSalesInvoice(int id,int goodsId)
        {
            return new GetSalesInvoiceDto
            {
                Id = id,
                CustomerName = "Saeed Ansari",
                Count = 5,
                SalesPrice = 2000,
                SalesDate = DateTime.Now.Date,
                GoodsId = goodsId
            };
        }
    }
}