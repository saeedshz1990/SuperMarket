using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Entities;
using SuperMarket.Services.Goodses.Contracts;

namespace SuperMarket.WebAPI.Controllers
{
    [Route("api/goodses")]
    [ApiController]
    public class GoodsesController : ControllerBase
    {
        private readonly GoodsService _goodsService;

        public GoodsesController(GoodsService goodsService)
        {
            _goodsService = goodsService;
        }

        [HttpPost]
        public void Add(AddGoodsDto dto)
        {
            _goodsService.Add(dto);
        }

        [HttpGet]
        public IList<GetGoodsDto> GetAll()
        {
            return _goodsService.GetAll();
        }
        
        [HttpGet("{id}")]
        public Goods GetById(int id)
        {
            return _goodsService.GetById(id);
        }

        [HttpPut("{id}")]
        public void Update(int id,UpdateGoodsDto dto)
        {
            _goodsService.Update(id, dto);
        }

        [HttpDelete("{id}")]
        public void Update(int id)
        {
            _goodsService.Delete(id);
        }
    }
}
