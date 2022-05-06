﻿using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Services.Goodses.Exceptions;

namespace SuperMarket.Services.Goodses
{
    public class GoodsAppService : GoodsService
    {
        private readonly GoodsRepository _goodsRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private Goods _goods;
        private AddGoodsDto _addGoodsDto;
        private Category _category;

        public GoodsAppService(UnitOfWork unitOfWork, GoodsRepository goodsRepository,
            CategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _goodsRepository = goodsRepository;
            _categoryRepository = categoryRepository;
        }

        public void Add(AddGoodsDto dto)
        {
            _goodsRepository.ExistName(dto.Name, dto.CategoryId);

            //bool getCategoryId = _categoryRepository.FindCategoryById(dto.CategoryId);
            //if (getCategoryId)
            //{
            //    throw new DuplicateGoodsNameInCategoryException();
            //}

            _goods = new Goods
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                UniqueCode = dto.UniqueCode,
                SalesPrice = dto.SalesPrice,
                MinimumInventory = dto.MinimumInventory,
                Count = dto.Count,
                SalesInvoiceId = 0,
                EntryDocumentId = 0
            };

            _goodsRepository.Add(_goods);
            _unitOfWork.Commit();
        }

        public Goods GetById(int id)
        {
            return _goodsRepository.GetById(id);
        }

        public IList<GetGoodsDto> GetAll()
        {
            return _goodsRepository.GetAll();
        }

        public void Delete(int id)
        {
            var goodsIdExist = _goodsRepository.ExistGoodsIdCheck(id);
            if (goodsIdExist)
            {
                throw new GoodsIdNotExistInThisCategoryException();
            }
            else
            {
                _goodsRepository.Delete(id);
                _unitOfWork.Commit();
            }
        }

        public void Update(int id, UpdateGoodsDto dto)
        {
            var goods = _goodsRepository.FindById(id);
            if (goods == null)
            {
                throw new DuplicateGoodsNameInCategoryException();
            }

            var checkGoods = _goodsRepository.ExistGoodsIdCheck(id);
            goods.Name = dto.Name;
            goods.CategoryId = dto.CategoryId;
            goods.UniqueCode = dto.UniqueCode;
            goods.SalesPrice = dto.SalesPrice;
            goods.MinimumInventory = dto.MinimumInventory;
            goods.Count = dto.Count;

            _unitOfWork.Commit();
        }
    }
}