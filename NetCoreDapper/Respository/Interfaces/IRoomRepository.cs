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
        Task<int> Create(Room room);

        Task<Room> Update(Room room);
        Task<int> Delete(int id);
    }
}
