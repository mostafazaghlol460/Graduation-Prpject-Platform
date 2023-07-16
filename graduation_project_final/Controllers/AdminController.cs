using graduation_project_final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace graduation_project_final.Controllers
{
    public class AdminController : Controller
    {
        graduation_project db = new graduation_project();
        // GET: Student
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user u)
        {            //we search to email and password and store in --- u 
            user u1 = db.users.Where(n => n.email == u.email && n.password == u.password).FirstOrDefault();
            //check if the input is admin or not 
            if (u.email == "admin1@gmail.com" && u.password == "00000000")
            {
                Session.Add("admin_id", 100);
                return RedirectToAction("Admin_of_student");
            }

            else
            {
                if (u1 != null)
                {
                    // check for input is enter for first time or entered before 
                    TempData["user_id"] = u1.id_user;
                    if (u1.role=="student" && u1.Activation == false)
                    {

                        // we transfer to page of oneshow
                        return RedirectToAction("register_student", "Student");
                    }
                    else if(u1.role == "doctor" && u1.Activation == false)
                    {
                        return RedirectToAction("register_doctor", "doctor");

                    }
                    else if (u1.role == "company" && u1.Activation == false)
                    {
                        //return RedirectToAction("register_company", "company");

                    }
                    else
                    {
                        if(u1.role=="student")
                        return RedirectToAction("home","Student");
                        else
                        return RedirectToAction("home", "Doctor");

                    }
                }
                // this is error in case happen error in entered eamil and password incorrect 

                ViewBag.stutas = "email and password is incorrect";
            }
            return View();
        }
        public ActionResult Admin_of_student()
        {

            if (Session["admin_id"] != null)
            {
                int ident = (int)Session["admin_id"];
                if (ident == 100)
                {

                    List<user> st = db.users.Where(n=>n.role=="student").ToList();
                    //ListAndData_of_student data = new ListAndData_of_student() { List_student = st };
                    User_Admin user1 = new User_Admin() { users_view = st };
                    return View(user1);
                }
            }
            ViewBag.stutas = "email and password is incorrect";
            return RedirectToAction("login");
        }
        [HttpPost]
        public ActionResult add_new_student(User_Admin u)
        {
            user u1 = u.users_input;
            if (ModelState.IsValid)
            {
                db.users.Add(u1);
                db.SaveChanges();
                return RedirectToAction("Admin_of_student");

            }
            return View();
        }
        public ActionResult edit_student(int id) 
        {
            user u = db.users.Where(n => n.id_user == id).FirstOrDefault();
            //User_Admin u2 = new User_Admin()
            //{
            //    users_input = u
            //};
            return View(u);
            //return RedirectToAction("admin_of_student",u);
        }
        public ActionResult update_student(user s)
        {
            db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Admin_of_student");

        }
        public ActionResult delete_student(int id)
        {
            user st = db.users.Where(n => n.id_user == id).SingleOrDefault();
            db.users.Remove(st);
            db.SaveChanges();
            return RedirectToAction("Admin_of_student");
        }

        // admin for doctor 

        public ActionResult Admin_of_doctor()
        {

            if (Session["admin_id"] != null)
            {
                int ident = (int)Session["admin_id"];
                if (ident == 100)
                {

                    List<user> st = db.users.Where(n => n.role == "doctor").ToList();
                    //ListAndData_of_student data = new ListAndData_of_student() { List_student = st };
                    User_Admin user1 = new User_Admin() { users_view = st };
                    return View(user1);
                }
            }
            ViewBag.stutas = "email and password is incorrect";
            return RedirectToAction("login");
        }
        [HttpPost]
        public ActionResult add_new_doctor(User_Admin u)
        {
            user u1 = u.users_input;
            if (ModelState.IsValid)
            {
                db.users.Add(u1);
                db.SaveChanges();
                return RedirectToAction("Admin_of_doctor");

            }
            return View();
        }
        public ActionResult edit_doctor(int id)
        {
            user u = db.users.Where(n => n.id_user == id &&  n.role == "doctor").FirstOrDefault();
            //User_Admin u2 = new User_Admin()
            //{
            //    users_input = u
            //};
            return View(u);
            //return RedirectToAction("admin_of_student",u);
        }
        public ActionResult update_doctor(user s)
        {
            db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Admin_of_doctor");

        }
        public ActionResult delete_doctor(int id)
        {
            user st = db.users.Where(n => n.id_user == id).SingleOrDefault();
            db.users.Remove(st);
            db.SaveChanges();
            return RedirectToAction("Admin_of_doctor");
        }

        // admin of company 

        public ActionResult Admin_of_company()
        {

            if (Session["admin_id"] != null)
            {
                int ident = (int)Session["admin_id"];
                if (ident == 100)
                {

                    List<user> st = db.users.Where(n => n.role == "company").ToList();
                    //ListAndData_of_student data = new ListAndData_of_student() { List_student = st };
                    User_Admin user1 = new User_Admin() { users_view = st };
                    return View(user1);
                }
            }
            ViewBag.stutas = "email and password is incorrect";
            return RedirectToAction("login");
        }
        [HttpPost]
        public ActionResult add_new_company(User_Admin u)
        {
            user u1 = u.users_input;
            if (ModelState.IsValid)
            {
                db.users.Add(u1);
                db.SaveChanges();
                return RedirectToAction("Admin_of_company");

            }
            return View();
        }
        public ActionResult edit_company(int id)
        {
            user u = db.users.Where(n => n.id_user == id && n.role == "company").FirstOrDefault();
            //User_Admin u2 = new User_Admin()
            //{
            //    users_input = u
            //};
            return View(u);
            //return RedirectToAction("admin_of_student",u);
        }
        public ActionResult update_company(user s)
        {
            db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Admin_of_company");

        }
        public ActionResult delete_company(int id)
        {
            user st = db.users.Where(n => n.id_user == id).SingleOrDefault();
            db.users.Remove(st);
            db.SaveChanges();
            return RedirectToAction("Admin_of_company");
        }
    }
}