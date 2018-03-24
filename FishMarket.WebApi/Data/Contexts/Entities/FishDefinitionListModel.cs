using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishMarket.WebApi.Data.Contexts.Entities
{
    public class FishDefinitionListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
