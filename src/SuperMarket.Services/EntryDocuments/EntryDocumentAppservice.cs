﻿using System.Collections.Generic;
using System.Threading;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.EntryDocuments.Contracts;
using SuperMarket.Services.EntryDocuments.Exceptions;

namespace SuperMarket.Services.EntryDocuments
{
    public class EntryDocumentAppservice : EntryDocumentService
    {
        private readonly EntryDocumentRepository _entryDocumentRepository;
        private readonly UnitOfWork _unitOfWork;

        public EntryDocumentAppservice(UnitOfWork unitOfWork,
            EntryDocumentRepository entryDocumentRepository)
        {
            _unitOfWork = unitOfWork;
            _entryDocumentRepository = entryDocumentRepository;
        }


        public void Add(AddEntryDocumentDto dto)
        {
            var isGoodsIdExist = _entryDocumentRepository.GetByGoodsId(dto.GoodsId);
            if (isGoodsIdExist == null)
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

        public IList<GetEntryDocumentDto> GetByGoodsId(int goodsId)
        {
            return _entryDocumentRepository.GetByGoodsId(goodsId);
        }

        public IList<GetEntryDocumentDto> GetAll()
        {
            return _entryDocumentRepository.GetAll();
        }

        public void Update(int id, UpdateEntryDocumentDto dto)
        {
            var entryDocument = _entryDocumentRepository.GetById(id);

            if (entryDocument == null)
            {
                throw new EntryDocumentIdNotFoundException();
            }
            entryDocument.GoodsCount = dto.GoodsCount;
            _unitOfWork.Commit();
        }
    }
}
