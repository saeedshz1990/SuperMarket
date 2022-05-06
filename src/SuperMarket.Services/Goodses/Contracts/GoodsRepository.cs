using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.Goodses.Contracts
{
    public interface GoodsRepository : Repository
    {
        void Add(Goods goods);
        void ExistName(string name, int categoryId);
        void Update(int id,Goods goods);
        void Delete(int id);
        Goods GetById(int id);
        Goods FindById(int id);
        IList<GetGoodsDto> GetAll();
        bool ExistGoodsIdCheck(int id);
    }
}

