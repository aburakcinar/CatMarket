using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Contexts.Entities;

namespace FishMarket.WebApi.Data.Interfaces
{
    public interface IFishDefinitionRepository
    {
        /// <summary>
        /// Lists all fish types
        /// </summary>
        /// <returns></returns>
        List<FishDefinitionListModel> ListAll();

        /// <summary>
        /// Finds fish type with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FishDefinition FindOne(int id);

        /// <summary>
        /// Creates new fish type with only name
        /// </summary>
        /// <param name="name"></param>
        void Add(string name);

        /// <summary>
        /// Creates new fish type with model
        /// </summary>
        /// <param name="entity"></param>
        void Add(FishDefinition entity);

        /// <summary>
        /// Updates existing fish type with model
        /// updates only name and price properties
        /// </summary>
        /// <param name="entity"></param>
        void Update(int id, FishDefinition entity);

        /// <summary>
        /// Deletes existing fish type
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Updates the price of the fish type with given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns>True if success</returns>
        void UpdatePrice(int id, decimal price);

        /// <summary>
        /// Uploads an image to an existing fish type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileType"></param>
        /// <param name="fileName"></param>
        /// <param name="length"></param>
        /// <param name="fileData"></param>
        void UploadImage(int id, string fileType, string fileName, long length, byte[] fileData);
    }
}
