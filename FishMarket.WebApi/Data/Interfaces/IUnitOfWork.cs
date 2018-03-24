using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishMarket.WebApi.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IFishDefinitionRepository FishDefinitionRepository { get; }

        int Save();
    }
}
