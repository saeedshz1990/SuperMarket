using System;
using SuperMarket.Entities;

namespace SuperMarket.Test.Tools.EntryDocumnets
{
    public class EntryDocumentBuilder
    {
        private EntryDocument _entryDocument;

        public EntryDocumentBuilder()
        {
            _entryDocument = new EntryDocument
            {
                DateBuy = DateTime.Now.Date,
                BuyPrice = 10000,
                GoodsCount = 10,
                GoodsId = 1
            };
        }

        
        public EntryDocument Build()
        {
            return _entryDocument;
        }
    }
}
