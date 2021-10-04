using Dapper;
using DapperCrud.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCrud.Abstraction
{
    public interface IDapper
    {
       

        Task<List<Student>> GetAllStudent();

        Task<Student> InsertStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<Student> DeleteStudent(int StudentId);
        Task<Student> GetStudentById(int StudentId);

        //T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        //List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        //int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        //T insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        //T update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
