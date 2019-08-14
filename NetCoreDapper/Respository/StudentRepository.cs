using Dapper;
using NetCoreDapper.Models;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SqlKata;
using NetCoreDapper.Respository.Interfaces;

namespace NetCoreDapper.Respository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connection)
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
        public async Task<int> Create(Student student)
        {
            var q = new Query(nameof(Student)).AsInsert(new { Name = student.Name, Family = student.Family, Phone = student.Phone });
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.ExecuteAsync(query.Sql, query.NamedBindings);
                return result;
            }

        }
        public async Task<Student> GetByID(int id)
        {

            var q = new Query(nameof(Data.Student)).Where(nameof(Data.Student.Id), id).Take(1);
            var query = new SqlServerCompiler().Compile(q);

            using (IDbConnection conn = Connection)
            {

                conn.Open();
                var result = await conn.QueryAsync<Student>(query.Sql, query.NamedBindings);
                return result.FirstOrDefault();
            }

        }
        public async Task<IEnumerable<Student>> GetRoom(Student filter)
        {
            var q = new Query(nameof(Student));
            if (filter != null)
            {
                if (filter.Id > 0)
                    q = q.Where(nameof(Student.Id), filter.Id);
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    q = q.WhereContains(nameof(Student.Name), filter.Name);
                if (!string.IsNullOrWhiteSpace(filter.Family))
                    q = q.WhereContains(nameof(Student.Family), filter.Family);
                if (!string.IsNullOrWhiteSpace(filter.Phone))
                    q = q.WhereContains(nameof(Student.Phone), filter.Phone);
            }
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Student>(query.Sql, query.NamedBindings.ToArray());
                return result;
            }
        }
        public async Task<Student> Update(Student student)
        {

            var q = new Query(nameof(Student)).Where(nameof(student.Id), student.Id).AsUpdate(new { Name = student.Name, Family = student.Family, Phone = student.Phone });
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.ExecuteAsync(query.Sql, query.NamedBindings);
                return student;
            }

        }

        public async Task<int> Delete(int id)
        {
            var q = new Query(nameof(Student)).Where(nameof(id), id).AsDelete();
            var query = new SqlServerCompiler().Compile(q);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.ExecuteAsync(query.Sql, query.NamedBindings);
                return result;
            }
        }
    }
}
