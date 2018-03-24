using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Contexts;
using FishMarket.WebApi.Data.Interfaces;

namespace FishMarket.WebApi.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private FishMarketContext _context;

        #endregion

        #region Properties

        public IFishDefinitionRepository FishDefinitionRepository { get; private set; }

        #endregion

        #region CTOR

        public UnitOfWork(FishMarketContext context)
        {
            _context = context;

            FishDefinitionRepository = new FishDefinitionRepository(_context);
        }

        #endregion

        #region Methods

        public int Save()
        {
            return _context.SaveChanges();
        }

        #endregion
    }
}
