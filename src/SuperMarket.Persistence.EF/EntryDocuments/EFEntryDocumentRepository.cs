using System.Collections.Generic;
using System.Linq;
using SuperMarket.Entities;
using SuperMarket.Services.EntryDocuments.Contracts;

namespace SuperMarket.Persistence.EF.EntryDocuments
{
    public class EFEntryDocumentRepository : EntryDocumentRepository
    {
        private readonly EFDataContext _context;

        public EFEntryDocumentRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(EntryDocument entryDocument)
        {
            _context.EntryDocuments.Add(entryDocument);
        }

        public IList<GetEntryDocumentDto> GetAll()
        {
            return _context.EntryDocuments.Select(_ => new GetEntryDocumentDto
            {
                id = _.Id,
                GoodsId = _.GoodsId,
                BuyPrice = _.BuyPrice,
                DateBuy = _.DateBuy.Date,
                GoodsCount = _.GoodsCount
            }).ToList();
        }

        public IList<GetEntryDocumentDto> GetByGoodsId(int goodsId)
        {
            return _context.EntryDocuments.Where(_ => _.GoodsId == goodsId).Select(
                _ => new GetEntryDocumentDto
                {
                    id = _.Id,
                    GoodsId = _.GoodsId,
                    BuyPrice = _.BuyPrice,
                    DateBuy = _.DateBuy.Date,
                    GoodsCount = _.GoodsCount
                }).ToList();
        }

        public EntryDocument GetById(int id)
        {
            return _context.EntryDocuments.FirstOrDefault(_ => _.Id == id);
        }

        public void Update(int id, EntryDocument entryDocument)
        {

        }
    }
}
