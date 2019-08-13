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
        public async Task<Room> GetByID(int id)
        {

            var q = new Query(nameof(Data.Room)).Where(nameof(Data.Room.Id),id).Take(1);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "Select * FROM Room WHERE ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Room>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }

        }
        public async Task<List<Room>> GetRoom(Room filter)
        {
            var q = new Query(nameof(Room));
            if (filter != null)
            {
                if (filter.Id > 0)
                    q = q.Where(nameof(Room.Id), filter.Id);
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    q = q.WhereLike(nameof(Room.Name), filter.Name);
                if (filter.StartDate > DateTime.MinValue)
                    q = q.Where(nameof(Room.StartDate), filter.StartDate);
                if (filter.EndDate > DateTime.MinValue)
                    q = q.Where(nameof(Room.EndDate), filter.EndDate);
            }
            var query =new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Room>(query.Sql, query.NamedBindings.ToArray());
                return result.ToList();
            }
        }
    }
}
