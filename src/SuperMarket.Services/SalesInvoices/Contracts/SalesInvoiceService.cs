using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.SalesInvoices.Contracts
{
    public interface SalesInvoiceService :Service
    {
        void Add(AddSalesInvoiceDto dto);
        IList<GetSalesInvoiceDto> GetAll();
        void Delete(int id);
        SalesInvoice GetById(int id);
        void Update(int id,UpdateSalesInvoiceDto dto);
    }
}
