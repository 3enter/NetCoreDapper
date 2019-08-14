using NetCoreDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDapper.Respository.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> GetByID(int id);
        Task<IEnumerable<Room>> Get(Room filter);
        Task<IEnumerable<Room>> GetProcedure(Room filter);       
        Task<int> Create(Room room);
        Task<int> CreateProcedure(Room room);
        Task<Room> Update(Room room);
        Task<Room> UpdateProcedure(Room room);      
        Task<int> Delete(int id);
        Task<int> DeleteProcedure(int id);
        Task<IEnumerable<Allocation>> GetAllocations();

    }
}
