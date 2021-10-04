using Dapper;
using DapperCrud.Abstraction;
using DapperCrud.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCrud.Repository
{
    public class DapperRepository : IDapper
    {
        public IConfiguration _iconfiguration;
        private readonly string connection;
        public DapperRepository(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            connection = iconfiguration["ConnectionString:MyConnection"];
        }


        public async Task<List<Student>> GetAllStudent()
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                var result = await con.QueryAsync<Student>("spStudentList", commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Student> InsertStudent(Student student)
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                con.Open();
                //SqlTransaction sqltrans = con.BeginTransaction();
                var param = new DynamicParameters();
                // param.Add("@StudentId", student.StudentId);
                param.Add("@StudentName", student.StudentName);
                param.Add("@StudentRollNo", student.StudentRollNo);
                var result = await con.QueryAsync("spInsertStudent", param, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                con.Open();
                //SqlTransaction sqltrans = con.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@StudentId", student.StudentId);
                param.Add("@StudentName", student.StudentName);
                param.Add("@StudentRollNo", student.StudentRollNo);
                var result = await con.QueryAsync("spUpdateStudent", param, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<Student> DeleteStudent(int StudentId)
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                con.Open();
                //SqlTransaction sqltrans = con.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@StudentId", StudentId);
                var result = await con.QueryAsync("spDeleteStudent", param, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<Student> GetStudentById(int StudentId)
        {
            //using (SqlConnection con = new SqlConnection(connection))
            using IDbConnection con = new SqlConnection(connection);
            {
                var param = new DynamicParameters();
                param.Add("@StudentId", StudentId);
                var result = await con.QueryFirstOrDefaultAsync<Student>("spGetStudentById", param, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }

        }

        public DbConnection GetDbConnecton()
        {
            throw new NotImplementedException();
        }

        
    }

  
    }


