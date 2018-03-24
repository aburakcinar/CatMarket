using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishMarket.WebUI.Models.FishDefinition
{
    public class FishDefinitionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] Picture { get; set; }

        public string PictureFileType { get; set; }

        public string PictureFileName { get; set; }

        public long? PictureFileLength { get; set; }
    }
}
