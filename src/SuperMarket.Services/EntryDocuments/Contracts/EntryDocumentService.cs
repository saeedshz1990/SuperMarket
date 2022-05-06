using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.Categories.Contracts;

namespace SuperMarket.Services.EntryDocuments.Contracts
{
    public interface EntryDocumentService :Service
    {
        void Add(AddEntryDocumentDto dto);
        EntryDocument GetById(int Id);
        IList<GetEntryDocumentDto> GetByGoodsId(int goodsId);
        IList<GetEntryDocumentDto> GetAll();
        void Update(int id, UpdateEntryDocumentDto dto);
    }
}
