using graduation_project_final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace graduation_project_final.Controllers
{
    public class StudentController : Controller
    {
        graduation_project db = new graduation_project();
        // GET: Student
        public ActionResult register_student()
        {
            //we start a session 

            if (TempData["user_id"] != null)
            {
                int id = (int)TempData["user_id"];
                Session.Add("id_user", id);
                user s = db.users.Where(n => n.id_user == id).FirstOrDefault();
                Register_st u1 = new Register_st()
                {
                    user_student = s
                };
                return View(u1);


            }
            else
            {
                //temp data is null but session id is still founded 
                //because he can return to this page without wrong 
                if (Session["id_user"] != null)
                {
                    int id = (int)Session["id_user"];
                    user s = db.users.Where(n => n.id_user == id).FirstOrDefault();
                    Register_st u1 = new Register_st()
                    {
                        user_student = s
                    };
                    return View(u1);

                }
                else
                {

                    return RedirectToAction("login","Admin");
                }
            }
            //session.add("id_user", id);
            //user s = db.users.Where(n => n.id_user == id).FirstOrDefault();
            //Register_st u1 = new Register_st() {
            //    user_student= s };

            return View();
        }

        [HttpPost]
        public ActionResult register_student(Register_st student1, string optradio, List<string> field_name, HttpPostedFileBase image)
        {
            user s = student1.user_student;
            organization org1 = student1.user_organization;
            


            if (image != null)
            {
                image.SaveAs(Server.MapPath("~/picture/" + image.FileName));

                s.image = image.FileName;
            }
            //student add = new student();

            foreach (var i in field_name)
            {

                org1.id_user = s.id_user;
                org1.skills = i;
                db.organizations.Add(org1);
               // db.Entry(org1).State = System.Data.Entity.EntityState.Modified;
            }
            s.Activation = true;

            db.Entry(s).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            //adding new  student in table student
            student s1 = new student();
            s1.id_organization = org1.id_organization;
            //if (ModelState.IsValid)
            //{
                db.students.Add(s1);
                db.SaveChanges();
            //}





            if (optradio == "Have Team")
            {
                s.type_team = "Have Team";
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("create_team");
            }
            else if (optradio == "NO Team")
            {
                s.type_team = "NO Team";
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("team_formation");
            }
            return View();
        }
        public ActionResult team_formation()
        {
            int id = (int)Session["id_user"];
            List<user> s = db.users.Where(n => n.type_team == "NO Team").ToList();

            List<organization> de = db.organizations.ToList();
            ViewBag.cv = de;
            return View(s);
        }
        public ActionResult create_team()
        {
            //int id = (int)Session["id_user"];
            int id = 7;
            organization user1=db.organizations.Where(n=>n.id_user==id).FirstOrDefault();
            student s1 = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();
            return View(s1);
        }


        public ActionResult join_team()
        {
            int id = (int)Session["id_user"];
            //int id = 7;
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s1 = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();
            return View(s1);
        }
        [HttpPost]
        public ActionResult join_team(student s1)
        {
            int id = (int)Session["id_user"];
            //int id = 7;
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();

            Group s2 = db.Groups.Where(n => n.group_id == s1.group_code).FirstOrDefault();
            if (s2.group_id != null)
            {
                s.group_code = s1.group_code;
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("teams");
            }
             ViewBag.stutas = "group code is incorrect";
            return View();
        }
        public ActionResult teams()
        {
            return Content("you join to team") ;
        }
        public ActionResult notify()
        {
            int id = (int)Session["id_user"];
            //int id = 7;
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();
            
            return View();
        }
       
        public ActionResult home()
        {
            
            if (TempData["user_id"] != null)
            {
                int id = (int)TempData["user_id"];
                Session.Add("id_user", id);
                return View();
            }else
            {
                if (Session["id_user"] != null)
                {
                    int id = (int)Session["id_user"];
                    //int id = 7;
                    organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
                    student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();
                    List<Request> r1 = db.Requests.Where(n => n.id_student == s.id_student && n.prof_accept == true).ToList();

                    if (r1.Count != 0)
                    {
                        ViewBag.message = r1;
                    }
                    return View();
                }

            }
            return RedirectToAction("login","Admin");


        }
        public ActionResult log_out()
        {
            Session["user_id"] = null;
            return RedirectToAction("login", "Admin");
        }







        // all function is about ideas 

        public ActionResult idea()
        {
            if (Session["id_user"] != null)
            {
                int id = (int)Session["id_user"];

                List<project> proj_D = db.projects.Where(n => n.company_supervisor == null && n.creator_id != null).ToList();
                List<project> proj_C = db.projects.Where(n => n.company_supervisor != null).ToList();



                show_project show1 = new show_project()
                {
                    project_company = proj_C,
                    project_doctor = proj_D

                };
                //to show all emils of Doctor

                List<user> u_doctor = db.users.Where(n => n.role == "doctor").ToList();
                SelectList st = new SelectList(u_doctor, "id_user", "email");
                ViewBag.doctor = st;

                //to show all emils of company 
                List<user> u_company = db.users.Where(n => n.role == "company").ToList();
                SelectList st_company = new SelectList(u_company, "id_user", "email");
                ViewBag.company = st_company;

                return View(show1);
            }
            return RedirectToAction("login", "Admin");
        }

        [HttpPost]
        public ActionResult suggest_idea_C(show_project ss1)
        {
            //this is id of student in table of user not student 

            int id = (int)Session["id_user"];



            //to show teble of project
            project proj1 = ss1.req_project_student;
            db.projects.Add(proj1);
            db.SaveChanges();

            //to get student-id 
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();

            //to show teble of request
            Request r1 = ss1.req1;
            r1.id_student = s.id_student;
            r1.id_project = proj1.id_project;
            // to get id of docotor in table staff 
            organization user_d = db.organizations.Where(n => n.id_user == r1.id_prof).FirstOrDefault();
            Staff doc1 = db.Staffs.Where(n => n.id_organization == user_d.id_organization).FirstOrDefault();
            r1.id_prof = doc1.id_staff;
            //to add new request 
            db.Requests.Add(r1);
            db.SaveChanges();
            return RedirectToAction("idea");

        }
        [HttpPost]
        public ActionResult suggest_idea_D(show_project ss1)
        {
            //this is id of student in table of user not student 

            int id = (int)Session["id_user"];



            //to show teble of project
            project proj1 = ss1.req_project_student;
            db.projects.Add(proj1);
            db.SaveChanges();

            //to get student-id 
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();

            //to show teble of request

            Request r1 = ss1.req1;
            r1.id_student = s.id_student;
            r1.id_project = proj1.id_project;
            // to get id of docotor in table staff 

            organization user_d = db.organizations.Where(n => n.id_user == r1.id_prof).FirstOrDefault();
            Staff doc1 = db.Staffs.Where(n => n.id_organization == user_d.id_organization).FirstOrDefault();
            r1.id_prof = doc1.id_staff;

            //to add new request 
            db.Requests.Add(r1);
            db.SaveChanges();
            return RedirectToAction("idea");

        }




        public ActionResult detail_doctor(int id)
        {
            project p1 = db.projects.Where(n => n.id_project == id).FirstOrDefault();
            List<user> u_doctor = db.users.Where(n => n.role == "doctor").ToList();
            SelectList st = new SelectList(u_doctor, "id_user", "email");
            ViewBag.cat = st;
            
            Req_student r1 = new Req_student()
            {
                project_show = p1
            };
            return View(r1);
        }
        [HttpPost]
        public ActionResult detail_doctor(Req_student r1)
        {
            Request req1 = r1.request1;
            project pp1 = db.projects.Where(n=>n.name== r1.project_show.name).FirstOrDefault();


            //this is id of student in table of user not student 

            int id = (int)Session["id_user"];

            //to get student-id 
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();
            //to show teble of request

            req1.id_student = s.id_student;
            req1.id_project = pp1.id_project;

            //to select id of staff from id of doctor in table staff
            organization user_d = db.organizations.Where(n => n.id_user == req1.id_prof).FirstOrDefault();
            Staff doc1 = db.Staffs.Where(n => n.id_organization == user_d.id_organization).FirstOrDefault();
            req1.id_prof = doc1.id_staff;

            //to add new request 
            db.Requests.Add(req1);
            db.SaveChanges();

            return RedirectToAction("idea");

        }
        public ActionResult detail_company(Req_student r1)
        {
            Request req1 = r1.request1;
            project pp1 = db.projects.Where(n => n.name == r1.project_show.name).FirstOrDefault();


            //this is id of student in table of user not student 

            int id = (int)Session["id_user"];

            //to get student-id 
            organization user1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            student s = db.students.Where(n => n.id_organization == user1.id_organization).FirstOrDefault();
            //to show teble of request

            req1.id_student = s.id_student;
            req1.id_project = pp1.id_project;

            //to select id of staff from id of doctor in table staff
            organization user_d = db.organizations.Where(n => n.id_user == req1.id_prof).FirstOrDefault();
            Staff doc1 = db.Staffs.Where(n => n.id_organization == user_d.id_organization).FirstOrDefault();
            req1.id_prof = doc1.id_staff;

            //to add new request 
            db.Requests.Add(req1);
            db.SaveChanges();

            return RedirectToAction("idea");

        }


        // for the profile of user - student
        public ActionResult profile()
        {
            if (Session["id_user"] != null)
            {
                int id = (int)Session["id_user"];
                user u1 = db.users.Where(n => n.id_user == id).FirstOrDefault();
                organization o1 = db.organizations.Where(n => n.id_user == u1.id_user).FirstOrDefault();
                student s1 = db.students.Where(n => n.id_organization == o1.id_organization).FirstOrDefault();
                string skills = o1.skills;
                string[] sk  = skills.Split('-');
                ViewBag.skills = sk;

                profile_display p1 = new profile_display()
                {
                    user1 = u1,
                    organization1=o1,
                    studnet1=s1

                };
                return View(p1);

            }
            return View();
        }
        public ActionResult edit_profile(int id)
        {
            user u1 = db.users.Where(n => n.id_user == id).FirstOrDefault();
            organization o1 = db.organizations.Where(n => n.id_user == u1.id_user).FirstOrDefault();
            student s1 = db.students.Where(n => n.id_organization == o1.id_organization).FirstOrDefault();
            string skills = o1.skills;
            string[] sk = skills.Split('-');
            ViewBag.skills = sk;

            profile_display p1 = new profile_display()
            {
                user1 = u1,
                organization1 = o1,
                studnet1 = s1

            };
            return View(p1);
        }

        [HttpPost]
        public ActionResult edit_profile(profile_display pp, List<string> field_name)
        {
            user u1_m = pp.user1;
            organization o1_m = pp.organization1;
            user u1 = db.users.Where(n => n.id_user == u1_m.id_user).FirstOrDefault();
            organization o1 = db.organizations.Where(n => n.id_user == u1_m.id_user).FirstOrDefault();
            u1.name = u1_m.name;
            u1.phone = u1_m.phone;
            u1.type_team = u1_m.type_team;
            u1.email = u1_m.email;
            o1.education = o1_m.education;

            //string skill = "";
            //    skill+= o1.skills;
            int len = field_name.Count;

            List<string> new_list = new List<string>();
            for(int i=0;i<len; i++)
            {

                if (field_name[i] == "")
                {
                    continue;
                }
                new_list.Add(field_name[i]);
                

            }
            var skill=string.Join("-", new_list);
            o1.skills = skill;

            db.SaveChanges();

            return RedirectToAction("home");
        }

        ////function to random value 
        //public string random()
        //{
        //    Random r = new Random();
        //    int prevnum = -1;
        //    int num;

        //    for (int loop = 0; loop < 10; loop++)
        //    {
        //        // If new random number same as previous then keep trying again until its different.
        //        do
        //        {
        //            num = r.Next(9);
        //        } while (num == prevnum);

        //        return Convert.ToString(num);


        //        prevnum = num;

        //    }

        //    return "";


        //}
    }
}