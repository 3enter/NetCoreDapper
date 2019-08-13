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
        Task<List<Room>> GetRoom(Room filter);
        Task<int> CreateRoom(Room room);
    }
}
