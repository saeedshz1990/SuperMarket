using System.Collections.Generic;

namespace SuperMarket.Entities
{
    public class Goods :EntityBase
    {
        public Goods()
        {
            EntryDocuments = new HashSet<EntryDocument>();
            SalesInvoices = new HashSet<SalesInvoice>();
        }
        
        
        public string Name { get; set; }
        public int Stock { get; set; }
        public int SalesPrice { get; set; }
        public string UniqueCode { get; set; }

        public HashSet<EntryDocument> EntryDocuments { get; set; }
        public HashSet<SalesInvoice> SalesInvoices { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}
