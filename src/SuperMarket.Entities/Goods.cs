using System.Collections.Generic;

namespace SuperMarket.Entities
{
    public class Goods :EntityBase
    {
       
        public string Name { get; set; }
        public int Count { get; set; }
        public int MinimumInventory { get; set; }
        public int SalesPrice { get; set; }
        public string UniqueCode { get; set; }

        public int EntryDocumentId { get; set; }
        public EntryDocument EntryDocuments { get; set; }

        public int SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoices { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}
