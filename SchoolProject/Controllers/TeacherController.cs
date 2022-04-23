﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher oneTeacher = controller.FindTeacher(id);
            return View(oneTeacher);
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/AddNewTeacher
        [HttpPost]
        public ActionResult AddNewTeacher(string fname, string lname, string emplnum, decimal salary)
        {

            Debug.WriteLine(fname);
            Debug.WriteLine(lname);
            Debug.WriteLine(emplnum);
            Debug.WriteLine(salary);

            Teacher newTeacher = new Teacher();
            newTeacher.FirstName = fname;
            newTeacher.LastName = lname;
            newTeacher.EmployeeNum = emplnum;
            newTeacher.Salary = salary;

            TeacherDataController controller = new TeacherDataController();
            // works
            controller.AddTeacherToDB(newTeacher);
            return RedirectToAction("List");

            //int addedTeacherId = controller.AddTeacher(newTeacher);
            //// modify to redirect to Show/id
            //return View("Show");

        }
        //GET : /Teacher/Delete
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher oneTeacher = controller.FindTeacher(id);
            return View(oneTeacher);

        }


        //POST : /Teacher/DeleteTeacher/{id}
        [HttpPost]
        public ActionResult DeleteTeacher(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacherFromDB(id);
            return RedirectToAction("List");

        }
    }
}