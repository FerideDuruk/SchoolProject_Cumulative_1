using System;
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

            // Web server redirects to List view showing all teachers
            // Views/Teacher/List.cshtml
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher oneTeacher = controller.FindTeacher(id);

            // Web server redirects to Show view showing only selected teacher
            // Views/Teacher/Show.cshtml
            return View(oneTeacher);
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            // Web server redirects to New view which only contains a form for teacher submission
            // Views/Teacher/New.cshtml
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
            controller.AddTeacherToDB(newTeacher);

            // Web server redirects to List view showing all teachers
            // Views/Teacher/List.cshtml
            return RedirectToAction("List");

            //int addedTeacherId = controller.AddTeacher(newTeacher);
            //// modify to redirect to Show/id
            //return View("Show");

        }
        //GET : /Teacher/Delete{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher oneTeacher = controller.FindTeacher(id);

            // Web server redirects to Delete view showing one teacher to be deleted
            // Views/Teacher/Delete.cshtml
            return View(oneTeacher);

        }


        //POST : /Teacher/DeleteTeacher/{id}
        [HttpPost]
        public ActionResult DeleteTeacher(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacherFromDB(id);

            // Web server redirects to List view showing all teachers
            // Views/Teacher/List.cshtml
            return RedirectToAction("List");

        }

        //GET : /Teacher/Edit{id}
        public ActionResult Edit(int id)
        {
            // Find teacher and pass the Teacher object to Edit view
            TeacherDataController controller = new TeacherDataController();
            Teacher oneTeacher = controller.FindTeacher(id);

            // Web server redirects to Delete view showing one teacher to be deleted
            // Views/Teacher/Edit.cshtml
            return View(oneTeacher);

        }

        //POST : /Teacher/UpdateTeacher/{id}
        /// <summary>
        /// Returns one teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// One Teacher object
        /// </returns>
        [HttpPost]
        public ActionResult UpdateTeacher(int id, string fname, string lname, string emplnum, decimal salary)
        {
            TeacherDataController controller = new TeacherDataController();

            Teacher newTeacher = new Teacher();
            newTeacher.Id = id;
            newTeacher.FirstName = fname;
            newTeacher.LastName = lname;
            newTeacher.EmployeeNum = emplnum;
            newTeacher.Salary = salary;

            controller.UpdateTeacherInDB(newTeacher);

            // Web server redirects to List view showing all teachers
            // Views/Teacher/List.cshtml
            return RedirectToAction("Show/"+id);

        }

    }
}