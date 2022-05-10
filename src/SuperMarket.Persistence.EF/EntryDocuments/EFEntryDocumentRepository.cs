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

        public List<GetEntryDocumentDto> GetAll()
        {
            return _context
                .EntryDocuments
                .Select(_ => new GetEntryDocumentDto
                {
                    Id = _.Id,
                    GoodsId = _.GoodsId,
                    BuyPrice = _.BuyPrice,
                    DateBuy = _.DateBuy.Date,
                    GoodsCount = _.GoodsCount
                }).ToList();
        }

        public IList<EntryDocument> GetByGoodsId(int goodsId)
        {
            return _context
                .EntryDocuments
                .Where(_ => _.GoodsId == goodsId)
                .Select(
                _ => new EntryDocument
                {
                    GoodsId = _.GoodsId,
                    BuyPrice = _.BuyPrice,
                    DateBuy = _.DateBuy.Date,
                    GoodsCount = _.GoodsCount
                }).ToList();
        }

        public EntryDocument FindByGoodsId(int goodsId)
        {
            return _context
                .EntryDocuments
                .FirstOrDefault(_ => _.GoodsId == goodsId);
        }

        public EntryDocument GetById(int id)
        {
            return _context
                .EntryDocuments
                .FirstOrDefault(_ => _.Id == id);
        }

        public void Update(int id, EntryDocument entryDocument)
        {

        }

        public bool GetByExistId(int id)
        {
            return _context
                .EntryDocuments
                .Any(_ => _.Id == id);
        }

        public bool CheckGoodsIdExist(int _goodsId)
        {
            return _context
                .EntryDocuments
                .Any(_ => _.GoodsId == _goodsId);
        }

        public IList<EntryDocument> GetListOfGoodsId(int goodsId)
        {
            return _context
                .EntryDocuments.
                Select(_ => new EntryDocument
                {
                    GoodsCount = _.GoodsCount,
                    GoodsId = _.GoodsId,
                    BuyPrice = _.BuyPrice,
                    DateBuy = _.DateBuy.Date
                }).Where(_ => _.GoodsId == goodsId)
                .ToList();
        }
    }
}
