using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.EntryDocuments.Contracts
{
    public interface EntryDocumentRepository :Repository
    {
        void Add(EntryDocument entryDocument);
        IList<GetEntryDocumentDto> GetAll();
        IList<GetEntryDocumentDto> GetByGoodsId(int goodsId);
        EntryDocument GetById(int id);
        void Update(int id, EntryDocument entryDocument);
    }
}
