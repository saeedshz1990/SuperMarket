using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Entities;
using SuperMarket.Services.SalesInvoices.Contracts;

namespace SuperMarket.WebAPI.Controllers
{
    [Route("api/salesinvoices")]
    [ApiController]
    public class SalesInvoicesController : ControllerBase
    {
        private readonly SalesInvoiceService _salesInvoiceService;

        public SalesInvoicesController(SalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }
        
        [HttpPost]
        public void Add(AddSalesInvoiceDto dto)
        {
            _salesInvoiceService.Add(dto);
        }

        [HttpGet]
        public IList<GetSalesInvoiceDto> GetAll()
        {
            return _salesInvoiceService.GetAll();
        }

        [HttpGet("{id}")]
        public SalesInvoice GetById(int id)
        {
            return _salesInvoiceService.GetById(id);
        }
        
        [HttpPut("{id}")]
        public void Update(int id, UpdateSalesInvoiceDto dto)
        {
            _salesInvoiceService.Update(id, dto);
        }
    }
}
