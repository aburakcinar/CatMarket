using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Interfaces;
using FishMarket.WebApi.Utils.MultipartHelper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FishMarket.WebApi.Controllers
{
    [Route("api/upload/v1/[action]")]
    public class UploadV1Controller : Controller
    {
        #region Fields

        private IUnitOfWork _unitOfWork;

        #endregion

        #region CTOR

        public UploadV1Controller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        //// POST api/values
        //[HttpPost, ActionName("new")]
        //public void UploadImage([FromBody]string value)
        //{
        //}

        [HttpPut("{id}"), ActionName("replace")]
        public IActionResult UploadReplace(int id)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest("Invalid content type.");
            }

            var file = Request.Form.Files.FirstOrDefault();

            if (file == null)
                return BadRequest("File not found.");

            using (MemoryStream ms = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(ms);

                _unitOfWork.FishDefinitionRepository.UploadImage(id, file.ContentType, file.FileName, file.Length, ms.ToArray());
                _unitOfWork.Save();
            }

            return Ok();
        }

        #endregion
    }
}
