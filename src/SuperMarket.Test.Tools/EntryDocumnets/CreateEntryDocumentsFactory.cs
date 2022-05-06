using System;
using SuperMarket.Entities;
using SuperMarket.Services.EntryDocuments.Contracts;

namespace SuperMarket.Test.Tools.EntryDocumnets
{
    public static class CreateEntryDocumentsFactory
    {
        public static EntryDocument CreateEntryDocument(int goodsId, int goodsCount, int buyPrice, DateTime dateTime)
        {
            return new EntryDocument()
            {
                GoodsId = goodsId,
                GoodsCount = goodsCount,
                BuyPrice = buyPrice,
                DateBuy = dateTime.Date,
            };
        }

        public static AddEntryDocumentDto CreateAddEntryDocumentDto(int goodsId)
        {
            return new AddEntryDocumentDto()
            {
                GoodsId = goodsId,
                GoodsCount = 7,
                BuyPrice = 2000,
                DateBuy = DateTime.Now.Date,
            };
        }
        public static AddEntryDocumentDto CreateAddEntryDocument(int goodsId, int goodsCount, int buyPrice,DateTime dateTime)
        {
            return new AddEntryDocumentDto()
            {
                GoodsId = goodsId,
                GoodsCount = goodsCount,
                BuyPrice = buyPrice,
                DateBuy = dateTime.Date,
            };
        }
        public static UpdateEntryDocumentDto CreateUpdateEntryDocumentDto(int goodsId, int goodsCount)
        {
            return new UpdateEntryDocumentDto()
            {
                GoodsCount = goodsCount,
            };
        }

        public static GetEntryDocumentDto CreateGetEntryDocument(int goodsId, int goodsCount, int buyPrice)
        {
            return new GetEntryDocumentDto()
            {
                GoodsId = goodsId,
                GoodsCount = goodsCount,
                BuyPrice = buyPrice,
                DateBuy = DateTime.Now.Date,
            };
        }
        public static GetEntryDocumentDto CreateGetEntryDocumentDto(int goodsId)
        {
            return new GetEntryDocumentDto()
            {
                GoodsId = goodsId,
                GoodsCount = 7,
                BuyPrice = 2000,
                DateBuy = DateTime.Now.Date,
            };
        }

    }
}
