using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Contexts;
using FishMarket.WebApi.Data.Contexts.Entities;
using FishMarket.WebApi.Data.Interfaces;

namespace FishMarket.WebApi.Data.Repositories
{
    public class FishDefinitionRepository : IFishDefinitionRepository
    {
        #region Fields

        private FishMarketContext _context;

        #endregion

        #region CTOR

        public FishDefinitionRepository(FishMarketContext context)
        {
            _context = context;
        }

        #endregion

        #region IFishDefinitionRepository

        public List<FishDefinitionListModel> ListAll()
        {
            return _context.FishDefinition.Select(p => new FishDefinitionListModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public FishDefinition FindOne(int id)
        {
            return _context.FishDefinition.FirstOrDefault(p => p.Id == id);
        }

        public void Add(string name)
        {
            _context.FishDefinition.Add(new FishDefinition { Name = name, Price = 0 });
        }

        public void Add(FishDefinition entity)
        {
            _context.FishDefinition.Add(entity);
        }

        public void UpdatePrice(int id, decimal price)
        {
            ///TODO : Due to performance concerns, it can be execute direct update command.
            ///
            var item = _context.FishDefinition.FirstOrDefault(p => p.Id == id);

            if(item != null)
            {
                item.Price = price;
            }
        }

        public void UploadImage(int id, string fileType, string fileName, long length, byte[] fileData)
        {
            var item = _context.FishDefinition.FirstOrDefault(p => p.Id == id);

            if (item != null)
            {
                item.Picture = fileData;
                item.PictureFileType = fileType;
                item.PictureFileName = fileName;
                item.PictureFileLength = length;
            }
        }

        #endregion
    }
}
