using System.Collections.Generic;
using System.Linq;
using SuperMarket.Entities;
using SuperMarket.Services.SalesInvoices.Contracts;

namespace SuperMarket.Persistence.EF.SalesInvoices
{
    public class EFSalesInvoiceRepository : SalesInvoiceRepository
    {
        private readonly EFDataContext _context;

        public EFSalesInvoiceRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(SalesInvoice salesInvoice)
        {
            _context.SalesInvoices.Add(salesInvoice);
        }

        public IList<GetSalesInvoiceDto> GetAll()
        {
            return _context.SalesInvoices.Select(x => new GetSalesInvoiceDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                Count = x.Count,
                SalesPrice = x.SalesPrice,
                SalesDate = x.SalesDate,
                GoodsId = x.GoodsId
            }).ToList();
        }

        public SalesInvoice GetById(int id)
        {
            return _context.SalesInvoices.FirstOrDefault(x => x.Id == id);
        }

        public void Update(SalesInvoice salesInvoice)
        {
       
        }

        public void Delete(int id)
        {
            var salesInvoice = FindById(id);
            _context.Remove(salesInvoice);
        }

        public SalesInvoice FindById(int id)
        {
            return _context.SalesInvoices.FirstOrDefault(x => x.Id == id);
        }
    }
}
