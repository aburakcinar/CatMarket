using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Contexts.Entities;
using FishMarket.WebApi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FishMarket.WebApi.Utils;

namespace FishMarket.WebApi.Controllers
{
    [Route("api/fishes/v1/[action]")]
    [Produces("application/json")]
    public class FishDefinitionV1Controller : Controller
    {
        #region Fields

        private IUnitOfWork _unitOfWork;

        #endregion

        #region CTOR

        public FishDefinitionV1Controller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        // GET api/values
        [HttpGet, ActionName("list")]
        public IEnumerable<FishDefinitionListModel> ListAll()
        {
            return _unitOfWork.FishDefinitionRepository.ListAll();
        }

        // GET api/values/5
        [HttpGet("{id}"), ActionName("get")]
        public FishDefinition Get(int id)
        {
            return _unitOfWork.FishDefinitionRepository.FindOne(id);
        }

        // POST api/values
        [HttpPost, ActionName("add")]
        public void Post([FromBody]FishDefinition model)
        {
            _unitOfWork.FishDefinitionRepository.Add(model);
            _unitOfWork.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}"), ActionName("price")]
        public IActionResult Put(int id, [FromBody]decimal price)
        {
            _unitOfWork.FishDefinitionRepository.UpdatePrice(id, price);
            var res = _unitOfWork.Save();
            /// TODO : responses must be improved. In case of error, it must return 400.

            if (res > 0)
                return Ok();
            else
                return NotFound();
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
