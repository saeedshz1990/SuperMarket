using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.EntryDocuments.Contracts
{
    public interface EntryDocumentRepository :Repository
    {
        void Add(EntryDocument entryDocument);
        List<GetEntryDocumentDto> GetAll();
        IList<EntryDocument> GetByGoodsId(int goodsId);
        EntryDocument FindByGoodsId(int goodsId);
        EntryDocument GetById(int id);
        void Update(int id, EntryDocument entryDocument);
        bool GetByExistId(int id);
        bool CheckGoodsIdExist(int _goodsId);
    }
}
