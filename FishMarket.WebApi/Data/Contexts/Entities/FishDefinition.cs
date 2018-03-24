using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FishMarket.WebApi.Data.Contexts.Entities
{
    [Table("fishdefinitions")]
    public class FishDefinition
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] Picture { get; set; }

        public string PictureFileType { get; set; }

        public string PictureFileName { get; set; }

        public long? PictureFileLength { get; set; }

    }
}
