using System.Collections.Generic;
using SQLitePCL;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Services.SalesInvoices.Contracts;
using SuperMarket.Services.SalesInvoices.Exceptions;

namespace SuperMarket.Services.SalesInvoices
{
    public class SalesInvoiceAppService : SalesInvoiceService
    {
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly GoodsRepository _goodsRepository;



        public SalesInvoiceAppService(UnitOfWork unitOfWork,
            SalesInvoiceRepository salesInvoiceRepository, GoodsRepository goodsRepository)
        {
            _unitOfWork = unitOfWork;
            _salesInvoiceRepository = salesInvoiceRepository;
            _goodsRepository = goodsRepository;
        }

        public void Add(AddSalesInvoiceDto dto)
        {
            var goodsIdCheck = _goodsRepository.GetById(dto.GoodsId);
            if (goodsIdCheck==null)
            {
                throw new GoodsIdNotFoundForSaleInvoicesException();//its warning
            }
        }

        public IList<GetSalesInvoiceDto> GetAll()
        {
            return _salesInvoiceRepository.GetAll();
        }

        public void Delete(int id)
        {
            var isCheckedExists = _salesInvoiceRepository.FindById(id);
            if (isCheckedExists == null)
            {
                throw new SalesInvoiceNotFoundException();
            }
            
            Goods goodsId = _goodsRepository
                .FindById(_salesInvoiceRepository.FindById(id)
                    .GoodsId);
            goodsId.Count = goodsId.Count + _salesInvoiceRepository.FindById(id).Count;
            _goodsRepository.Update(goodsId.Id, goodsId);
            _salesInvoiceRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public SalesInvoice GetById(int id)
        {
            return _salesInvoiceRepository.GetById(id);
        }

        public void Update(int id, UpdateSalesInvoiceDto dto)
        {
            var isCheckedExists = _salesInvoiceRepository.FindById(id);
            if (isCheckedExists == null)
            {
                throw new SalesInvoiceNotFoundException();
            }

            Goods goodsId = _goodsRepository
                .FindById(_salesInvoiceRepository.FindById(id)
                    .GoodsId);
            goodsId.Count = goodsId.Count - _salesInvoiceRepository.FindById(id).Count;

            _goodsRepository.Update(goodsId.Id, goodsId);

            var salesInvoice = _salesInvoiceRepository.FindById(id);
            salesInvoice.GoodsId = dto.GoodsId;
            salesInvoice.Count = dto.Count;
            _salesInvoiceRepository.Update(salesInvoice);
            _unitOfWork.Commit();
        }
    }
}
