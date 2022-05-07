using System.Collections.Generic;
using System.Linq;
using SuperMarket.Entities;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Goodses.Contracts;

namespace SuperMarket.Persistence.EF.Goodses
{
    public class EFGoodsRepository : GoodsRepository
    {
        private readonly EFDataContext _context;

        public EFGoodsRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Goods goods)
        {
            _context.Goods.Add(goods);
        }

        public void ExistName(string name, int categoryId)
        {
            _context.Goods.Any(_ => _.Name == name && _.CategoryId == categoryId);
        }

        public void Update(int id, Goods goods)
        {

        }

        public void Delete(int id)
        {
            var goods = _context.Goods.FirstOrDefault(_ => _.Id == id);
            _context.Remove(goods);
        }

        public Goods GetById(int id)
        {
            var goods = _context.Goods.FirstOrDefault(_ => _.Id == id);
            return goods;
        }

        public Goods FindById(int id)
        {
            return _context.Goods.FirstOrDefault(_ => _.Id == id);
        }

        public IList<GetGoodsDto> GetAll()
        {
            return _context.Goods.Select(_ => new GetGoodsDto
            {
                Id = _.Id,
                Name = _.Name,
                CategoryId = _.CategoryId,
                UniqueCode = _.UniqueCode,
                SalesPrice = _.SalesPrice,
                MinimumInventory = _.MinimumInventory,
                Count = _.Count
            }).ToList();
        }

        public bool ExistGoodsIdCheck(int id)
        {
            return _context.Goods.Any(_ => _.Id == id);
        }

        public int CountGoodsInCategory(int categoryId)
        {
            return _context.Goods.Count(_ => _.CategoryId == categoryId);
        }

        public IList<Goods> FindCategoryGoods(int categoryId)
        {
            return _context
                .Goods
                .Where(_ => _.CategoryId == categoryId)
                .Select(_ => new Goods
                {
                    Id = _.Id,
                    Name = _.Name,
                    CategoryId = _.CategoryId,
                    UniqueCode = _.UniqueCode,
                    SalesPrice = _.SalesPrice,
                    MinimumInventory = _.MinimumInventory,
                    Count = _.Count
                }).ToList();
        }
    }
}
