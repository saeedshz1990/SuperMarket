using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.EntryDocuments.Contracts;
using SuperMarket.Services.EntryDocuments.Exceptions;
using SuperMarket.Services.Goodses.Contracts;

namespace SuperMarket.Services.EntryDocuments
{
    public class EntryDocumentAppService : EntryDocumentService
    {
        private readonly EntryDocumentRepository _entryDocumentRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly GoodsRepository _goodsRepository;

        public EntryDocumentAppService(UnitOfWork unitOfWork,
            EntryDocumentRepository entryDocumentRepository, GoodsRepository goodsRepository)
        {
            _unitOfWork = unitOfWork;
            _entryDocumentRepository = entryDocumentRepository;
            _goodsRepository = goodsRepository;
        }

        public void Add(AddEntryDocumentDto dto)
        {
            bool isGoodsIdExist = _entryDocumentRepository.CheckGoodsIdExist(dto.GoodsId);
            if (isGoodsIdExist)
            {
                throw new GoodIdNotFoundException();
            }
            
            var entryDocument = new EntryDocument
            {
                GoodsId = dto.GoodsId,
                GoodsCount = dto.GoodsCount,
                BuyPrice = dto.BuyPrice,
                DateBuy = dto.DateBuy
            };

            _entryDocumentRepository.Add(entryDocument);
            _unitOfWork.Commit();
        }

        public EntryDocument GetById(int Id)
        {
            return _entryDocumentRepository.GetById(Id);
        }

        public IList<EntryDocument> GetByGoodsId(int goodsId)
        {
            return _entryDocumentRepository.GetByGoodsId(goodsId);
        }

        public List<GetEntryDocumentDto> GetAll()
        {
            return _entryDocumentRepository.GetAll();
        }

        public void Update(int id, UpdateEntryDocumentDto dto)
        {
            bool entryDocumentId = _entryDocumentRepository.GetByExistId(id);
            if (!entryDocumentId)
            {
                throw new EntryDocumentIdNotFoundException();
            }
            EntryDocument entryDocument = _entryDocumentRepository.GetById(id);
            entryDocument.GoodsCount = dto.GoodsCount;
            _unitOfWork.Commit();
        }
    }
}