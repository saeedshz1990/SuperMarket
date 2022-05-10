using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Entities;
using SuperMarket.Services.EntryDocuments.Contracts;

namespace SuperMarket.WebAPI.Controllers
{
    [Route("api/entrydocuments")]
    [ApiController]
    public class EntryDocumentsController : ControllerBase
    {
        private readonly EntryDocumentService _entryDocumentService;

        public EntryDocumentsController(EntryDocumentService entryDocumentService)
        {
            _entryDocumentService = entryDocumentService;
        }

        [HttpPost]
        public void Add(AddEntryDocumentDto dto)
        {
            _entryDocumentService.Add(dto);
        }

        [HttpGet]
        public IList<GetEntryDocumentDto> GetAll()
        {
            return _entryDocumentService.GetAll();
        }

        [HttpGet("{id}")]
        public EntryDocument GetById(int id)
        {
            return _entryDocumentService.GetById(id);
        }

        [HttpPut("{id}")]
        public void Update(int id, UpdateEntryDocumentDto dto)
        {
            _entryDocumentService.Update(id, dto);
        }
    }
}
