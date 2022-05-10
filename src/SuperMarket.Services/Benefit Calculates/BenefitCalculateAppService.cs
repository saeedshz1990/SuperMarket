using System.Linq;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.Benefit_Calculates.Contracts;
using SuperMarket.Services.Benefit_Calculates.Exceptions;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.EntryDocuments.Contracts;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Services.SalesInvoices.Contracts;

namespace SuperMarket.Services.Benefit_Calculates
{
    public class BenefitCalculateAppService : BenefitCalculateService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private readonly GoodsRepository _goodsRepository;
        private readonly EntryDocumentRepository _entryDocumentRepository;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;

        private EntryDocument _entryDocument;
        private SalesInvoice _salesInvoice;
        private int goodsBenefit;
        private Goods _goods;
        public BenefitCalculateAppService(UnitOfWork unitOfWork,
            CategoryRepository categoryRepository, GoodsRepository goodsRepository,
            EntryDocumentRepository entryDocumentRepository,
            SalesInvoiceRepository salesInvoiceRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _goodsRepository = goodsRepository;
            _entryDocumentRepository = entryDocumentRepository;
            _salesInvoiceRepository = salesInvoiceRepository;
        }

        public int BenefitGoodsCalculate(int goodsId)
        {
            var goodsCount = _entryDocumentRepository
                .GetByGoodsId(goodsId)
                .ToList()
                .FirstOrDefault(_ => _.GoodsId == goodsId);

            var counter = _entryDocumentRepository
                .GetByGoodsId(goodsId)
                .Count;

            var goodsSell = _salesInvoiceRepository
                .FindGoodsId(goodsId)
                .ToList()
                .FirstOrDefault(_ => _.GoodsId == goodsId);

            int counterSell = _salesInvoiceRepository
                .FindGoodsId(goodsId)
                .Count;


            for (int i = 0; i > counter; i++)
            {
                var calculateEntryBenefit = goodsCount.GoodsCount * goodsCount.BuyPrice;

                for (int j = 0; j > counterSell; j++)
                {
                    var CalculateSalesBenefit = goodsSell.Count * goodsSell.SalesPrice;

                    goodsBenefit = CalculateSalesBenefit - calculateEntryBenefit;

                }
            }

            return goodsBenefit;

            
        }

        public int BenefitCategoryCalculate(int categoryId,int goodsId)
        {
            var listOfCategories = _goodsRepository.GetListOfCategory(categoryId);
   
            if (listOfCategories == null)
            {
                throw new ListOfCategoryNotFoundForCalculateBenefitException();
            }
            else
            {
                var goodsCount = _entryDocumentRepository
                    .GetByGoodsId(goodsId)
                    .ToList()
                    .FirstOrDefault(_ => _.GoodsId == goodsId);

                var counter = _entryDocumentRepository
                    .GetByGoodsId(goodsId)
                    .Count;

                var goodsSell = _salesInvoiceRepository
                    .FindGoodsId(goodsId)
                    .ToList()
                    .FirstOrDefault(_ => _.GoodsId == goodsId);

                int counterSell = _salesInvoiceRepository
                    .FindGoodsId(goodsId)
                    .Count;


                for (int i = 0; i > counter; i++)
                {
                    var calculateEntryBenefit = goodsCount.GoodsCount * goodsCount.BuyPrice;

                    for (int j = 0; j > counterSell; j++)
                    {
                        var CalculateSalesBenefit = goodsSell.Count * goodsSell.SalesPrice;

                        goodsBenefit = CalculateSalesBenefit - calculateEntryBenefit;

                    }
                }

                return goodsBenefit;
            }

        }
    }
}