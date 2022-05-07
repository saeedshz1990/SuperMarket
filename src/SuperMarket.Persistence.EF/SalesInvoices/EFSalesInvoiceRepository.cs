using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return _context.SalesInvoices
                .Select(_ => new SalesInvoice
                {
                    Id = _.Id,
                    CustomerName = _.CustomerName,
                    Count = _.Count,
                    SalesPrice = _.SalesPrice,
                    SalesDate = _.SalesDate,
                    GoodsId = _.GoodsId
                    })
                .FirstOrDefault(x => x.Id == id);


        }

        public bool GetBySalesInvoicesId(int id)
        {
            return _context.SalesInvoices.Any(x => x.Id == id);
        }

        public int FindByGoodsId(int goodsCategoryId)
        {
            return _context.SalesInvoices.Count(x => x.GoodsId == goodsCategoryId);
        }

        public IList<SalesInvoice> FindGoodsId(int goodsId)
        {
            return _context.SalesInvoices.Where(x => x.GoodsId == goodsId).ToList();
        }
    }
}
