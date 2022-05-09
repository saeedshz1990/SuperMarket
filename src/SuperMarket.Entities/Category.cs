using System.Collections.Generic;

namespace SuperMarket.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {
            Goods = new HashSet<Goods>();
        }

        public string Name { get; set; }

        public HashSet<Goods> Goods { get; set; }
    }
}
