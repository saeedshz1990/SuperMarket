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
        
    }
}
