using Dapper;
using DapperCrud.Abstraction;
using DapperCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
       
        public IConfiguration _iconfiguration;
        private readonly string connection;
        private readonly IDapper _dapper;
        public StudentApiController(IConfiguration iconfiguration, IDapper dapper)
        {            
            _iconfiguration = iconfiguration;
            connection = iconfiguration["ConnectionString:MyConnection"];
            _dapper = dapper;
        }
        // GET: StudentApi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var student = await _dapper.GetAllStudent();
            return Ok(student);
        }
        // GET: StudentApi/5
        [HttpGet("{id}", Name = "StudentById")]
        public async Task<ActionResult<Student>> Details(int id)
        {
            var student = await _dapper.GetStudentById(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        // POST: StudentApi
        [HttpPost("Create")]      
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                var InsertStudent = await _dapper.InsertStudent(student);
                return Ok(student);
                //return CreatedAtRoute("StudentById", new { id = InsertStudent.StudentId }, InsertStudent);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: StudentApi/5
        [HttpPut("{id}")]       
        public async Task<IActionResult> Edit(int id, Student student)
        {
            try
            {
                
                var dbStudent = await _dapper.GetStudentById(id);
                if (dbStudent == null)
                    return NotFound();
                await _dapper.UpdateStudent(student);
                return Ok(student);



            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // HttpDelete: StudentApi/5
        [HttpDelete("{id}")]       
        public async Task<ActionResult<Student>> Delete(int id)
        {
            try
            {
                var dbStudent = await _dapper.GetStudentById(id);
                if (dbStudent == null)
                    return NotFound();
                await _dapper.DeleteStudent(id);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

