using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.Goodses.Contracts
{
    public interface GoodsService :Service
    {
        void Add(AddGoodsDto dto);
        Goods GetById(int id);
        IList<GetGoodsDto> GetAll();
        void Delete(int id);
        void Update(int id, UpdateGoodsDto dto);
    }
}
