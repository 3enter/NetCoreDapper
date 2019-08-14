using Dapper;
using Microsoft.Extensions.Configuration;

using NetCoreDapper.Models;
using NetCoreDapper.Respository.Interfaces;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDapper.Respository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString;

        public RoomRepository(string connection)
        {
            _connectionString = connection;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
        public async Task<int> Create(Room room)
        {
            var q = new Query(nameof(Room)).AsInsert(new { Name = room.Name, StartDate = room.StartDate, EndDate = room.EndDate });
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.ExecuteAsync(query.Sql, query.NamedBindings);
                return result;
            }

        }
        public async Task<Room> GetByID(int id)
        {

            var q = new Query(nameof(Data.Room)).Where(nameof(Data.Room.Id), id).Take(1);
            var query = new SqlServerCompiler().Compile(q);

            using (IDbConnection conn = Connection)
            {

                conn.Open();
                var result = await conn.QueryAsync<Room>(query.Sql, query.NamedBindings);
                return result.FirstOrDefault();
            }

        }
        public async Task<IEnumerable<Room>> Get(Room filter)
        {
            var q = new Query(nameof(Room));
            if (filter != null)
            {
                if (filter.Id > 0)
                    q = q.Where(nameof(Room.Id), filter.Id);
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    q = q.WhereContains(nameof(Room.Name), filter.Name);
                if (filter.StartDate > DateTime.MinValue)
                    q = q.Where(nameof(Room.StartDate), filter.StartDate);
                if (filter.EndDate > DateTime.MinValue)
                    q = q.Where(nameof(Room.EndDate), filter.EndDate);
            }
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Room>(query.Sql, query.NamedBindings.ToArray());
                return result;
            }
        }
        public async Task<Room> Update(Room room)
        {

            var q = new Query(nameof(Room)).Where(nameof(room.Id), room.Id).AsUpdate(new { Name = room.Name, StartDate = room.StartDate, EndDate = room.EndDate });
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.ExecuteAsync(query.Sql, query.NamedBindings);
                return room;
            }

        }

        public async Task<int> Delete(int id)
        {
            var q = new Query(nameof(Room)).Where(nameof(id), id).AsDelete();
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.ExecuteAsync(query.Sql, query.NamedBindings);
                return result;
            }
        }

        public async Task<IEnumerable<Allocation>> GetAllocations()
        {
            var allocationDictionary = new Dictionary<long, Allocation>();

            var q = new Query(nameof(Room)).Join(nameof(NetCoreDapper.Data.StudentRoom), nameof(Room)+"."+nameof(Room.Id), nameof(Data.StudentRoom.RoomId)).Join(nameof(Student), nameof(Data.StudentRoom.StudentId), nameof(Student) + "." + nameof(Student.Id));

            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Room, Data.StudentRoom, Student, Allocation>(query.Sql, (room, studentroom, student) =>
                {
                    Allocation allocation; 

                    if (!allocationDictionary.TryGetValue(room.Id, out allocation))
                    {
                        allocation=  new Allocation { Id = room.Id, EndDate = room.EndDate, Name = room.Name, StartDate = room.StartDate };
                        allocation.Details = new List<AllocationDetail>();
                        allocationDictionary.Add(allocation.Id, allocation);
                    }
                    allocation.Details.Add(new AllocationDetail { Id = studentroom.Id, RoomId = room.Id, StudentFamily = student.Family, StudentId = student.Id, StudentName = student.Name });
                    return allocation;
                }, query.NamedBindings.ToArray());
                return result.Distinct();
            }
        }
    }
}
