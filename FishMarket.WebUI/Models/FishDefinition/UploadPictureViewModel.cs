using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FishMarket.WebUI.Models.FishDefinition
{
    public class UploadPictureViewModel
    {
        public IFormFile PictureFile { get; set; }
        public int Id { get; set; }
    }
}
