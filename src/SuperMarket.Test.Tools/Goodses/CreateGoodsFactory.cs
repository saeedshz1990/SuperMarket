using SuperMarket.Entities;
using SuperMarket.Services.Goodses.Contracts;

namespace SuperMarket.Test.Tools.Goodses
{
    public static class CreateGoodsFactory
    {
        public static Goods CreateGoodsDto(string name, int count, int minimumInventory
            , int salesPrice, string uniqueCode, int categoryId)
        {
            return new Goods
            {
                Name = name,
                Count = count,
                MinimumInventory = minimumInventory,
                SalesPrice = salesPrice,
                UniqueCode = uniqueCode,
                CategoryId = categoryId
            };
        }

        public static Goods CreateGoods(int categoryId)
        {
            return new Goods
            {
                Name = "ماست رامک",
                Count = 10,
                MinimumInventory = 5,
                SalesPrice = 2000,
                UniqueCode = "YR-190",
                CategoryId = categoryId
            };
        }
        public static AddGoodsDto CreateAddGoods(int categoryId)
        {
            return new AddGoodsDto
            {
                Name = "ماست رامک",
                Count = 10,
                MinimumInventory = 5,
                SalesPrice = 2000,
                UniqueCode = "YR-190",
                CategoryId = categoryId
            };
        }

        public static AddGoodsDto CreateAddGoodsDto(string name, int count, int minimumInventory
            , int salesPrice, string uniqueCode, int categoryId)
        {
            return new AddGoodsDto
            {
                Name = name,
                Count = count,
                MinimumInventory = minimumInventory,
                SalesPrice = salesPrice,
                UniqueCode = uniqueCode,
                CategoryId = categoryId
            };
        }

        public static UpdateGoodsDto CreateUpdateGoodsDto(string name, int count, int minimumInventory
            , int salesPrice, string uniqueCode, int categoryId)
        {
            return new UpdateGoodsDto
            {
                Name = name,
                Count = count,
                MinimumInventory = minimumInventory,
                SalesPrice = salesPrice,
                UniqueCode = uniqueCode,
                CategoryId = categoryId
            };
        }

        public static UpdateGoodsDto CreateUpdateGoods( int categoryId)
        {
            return new UpdateGoodsDto
            {
                Name = "ماست رامک",
                Count = 10,
                MinimumInventory = 5,
                SalesPrice = 2000,
                UniqueCode = "YR-190",
                CategoryId = categoryId
            };
        }

        public static GetGoodsDto CreateGetGoodsDto(string name, int count, int minimumInventory
            , int salesPrice, string uniqueCode, int categoryId)
        {
            return new GetGoodsDto
            {
                Name = name,
                Count = count,  
                MinimumInventory = minimumInventory,
                SalesPrice = salesPrice,
                UniqueCode = uniqueCode,
                CategoryId = categoryId
            };
        }

        public static GetGoodsDto CreateGetGoods(int categoryId)
        {
            return new GetGoodsDto
            {
                Name = "ماست رامک",
                Count = 10,
                MinimumInventory = 5,
                SalesPrice = 2000,
                UniqueCode = "YR-190",
                CategoryId = categoryId
            };
        }

    }
}
