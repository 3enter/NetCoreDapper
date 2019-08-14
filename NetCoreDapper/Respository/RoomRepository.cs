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
    }
}
