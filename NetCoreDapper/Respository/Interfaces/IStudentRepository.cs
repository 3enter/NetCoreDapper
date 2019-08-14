using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NetCoreDapper.Models;

namespace NetCoreDapper.Respository.Interfaces
{
    public interface IStudentRepository
    {
        IDbConnection Connection { get; }

        Task<int> Create(Student student);
        Task<int> Delete(int id);
        Task<Student> GetByID(int id);
        Task<IEnumerable<Student>> GetRoom(Student filter);
        Task<Student> Update(Student student);
    }
}