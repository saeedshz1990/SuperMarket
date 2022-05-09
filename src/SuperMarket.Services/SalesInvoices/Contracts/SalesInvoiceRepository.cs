using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.SalesInvoices.Contracts
{
    public interface SalesInvoiceRepository :Repository
    {
        void Add(SalesInvoice salesInvoice);
        IList<GetSalesInvoiceDto> GetAll();
        SalesInvoice GetById(int id);
        void Update(SalesInvoice salesInvoice);
        void Delete(int id);
        SalesInvoice FindById(int id);
        bool GetBySalesInvoicesId(int id);
        int FindByGoodsId(int goodsCategoryId);
        bool GoodsIdCheckForExistence(int goodsId);
        IList<SalesInvoice> FindGoodsId(int goodsId);


        bool FindByIds(int id);
    }
}
