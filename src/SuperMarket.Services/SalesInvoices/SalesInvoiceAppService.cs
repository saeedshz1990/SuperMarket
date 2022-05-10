using System.Collections.Generic;
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
            var checkGoodsId = _salesInvoiceRepository.GoodsIdCheckForExistence(dto.GoodsId);
            if (!checkGoodsId)
            {
                throw new GoodsIdNotFoundForSaleInvoicesException();
            }

            var salesInvoices = new SalesInvoice
            {
                GoodsId = dto.GoodsId,
                Count = dto.Count,
                SalesPrice = dto.SalesPrice,
                SalesDate = dto.SalesDate,
                CustomerName = dto.CustomerName,
            };

            _salesInvoiceRepository.Add(salesInvoices);
            _unitOfWork.Commit();
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
            _salesInvoiceRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public SalesInvoice GetById(int id)
        {
            return _salesInvoiceRepository.GetById(id);
        }

        public void Update(int id, UpdateSalesInvoiceDto dto)
        {
            var _salesInvoices = new SalesInvoice();
            _salesInvoices = _salesInvoiceRepository.FindById(id);

            bool isCheckedExists = _salesInvoiceRepository.FindByIds(id);
            if (!isCheckedExists)
            {
                throw new SalesInvoicesNotExistException();
            }
            _salesInvoices.GoodsId = dto.GoodsId;
            _salesInvoices.Count = dto.Count;
            _unitOfWork.Commit();
        }

    }
}