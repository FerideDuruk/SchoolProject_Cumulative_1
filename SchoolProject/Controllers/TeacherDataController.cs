using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject.Models;
using MySql.Data.MySqlClient;

namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        // Created an object School of SchoolDbContext class which allows to access MySQL Database
        private SchoolDbContext School = new SchoolDbContext();

        // TeacherDataController will access the teachers table of the school database.
        /// <summary>
        /// Returns a list of all teachers in the database
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers objects
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            // Created object dbConnection of MySqlConnection type
            MySqlConnection dbConnection = School.AccessDatabase();

            // Opened the connection between the web server and database
            dbConnection.Open();

            // Created a new command for the school database
            MySqlCommand query = dbConnection.CreateCommand();

            // Specified MySQL query
            query.CommandText = "Select * from teachers";

            // Saved results of the query into the variable queryResult
            MySqlDataReader queryResult = query.ExecuteReader();

            // Created an empty list of Teacher objects
            List<Teacher> listTeachers = new List<Teacher> { };

            // Used while loop to go through each record of the queryResult object
            while (queryResult.Read())
            {
                // Obtained teacher's id, first name, last name, salary and hire date from queryResult object
                string teacherFirstName = queryResult["teacherfname"].ToString();
                string teacherLastName = queryResult["teacherlname"].ToString();
                int teacherSalary = Convert.ToInt32(queryResult["salary"]);
                int teacherId = Convert.ToInt32(queryResult["teacherid"]);
                DateTime teacherHireDate = Convert.ToDateTime(queryResult["hiredate"]);

                Teacher eachTeacher = new Teacher();
                eachTeacher.Id = teacherId;
                eachTeacher.FirstName = teacherFirstName;
                eachTeacher.LastName = teacherLastName;
                eachTeacher.Salary = teacherSalary;
                eachTeacher.HireDate = teacherHireDate;

                // Added each teacher's object that includes id, first name, last name, salary and hire date to the list
                listTeachers.Add(eachTeacher);
            }

            // Close the connection between the MySQL Database and the WebServer
            dbConnection.Close();

            // Returned the list of teachers objects
            return listTeachers;
        }

        /// <summary>
        /// Returns one teacher from the database
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher/{id}</example>
        /// <returns>
        /// One teacher's object
        /// </returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher eachTeacher = new Teacher();

            // Created object dbConnection of MySqlConnection type
            MySqlConnection dbConnection = School.AccessDatabase();

            // Opened the connection between the web server and database
            dbConnection.Open();

            // Created a new command for the school database
            MySqlCommand query = dbConnection.CreateCommand();

            // Specified MySQL query
            query.CommandText = "Select * from teachers where teacherid = " + id;

            // Saved results of the query into the variable queryResult
            MySqlDataReader queryResult = query.ExecuteReader();

            // Read data from queryResult
            while (queryResult.Read())
            {
                // Obtained teacher's id, first name, last name, salary and hire date from queryResult object
                string teacherFirstName = queryResult["teacherfname"].ToString();
                string teacherLastName = queryResult["teacherlname"].ToString();
                int teacherSalary = Convert.ToInt32(queryResult["salary"]);
                int teacherId = Convert.ToInt32(queryResult["teacherid"]);
                DateTime teacherHireDate = Convert.ToDateTime(queryResult["hiredate"]);

                eachTeacher.Id = teacherId;
                eachTeacher.FirstName = teacherFirstName;
                eachTeacher.LastName = teacherLastName;
                eachTeacher.Salary = teacherSalary;
                eachTeacher.HireDate = teacherHireDate;
            }

            // Close the connection between the MySQL Database and the WebServer
            dbConnection.Close();

            // Returned teacher's object that includes id, first name, last name, salary and hire date
            return eachTeacher;
        }


    }
}
