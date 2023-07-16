using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using graduation_project_final.Models;


namespace graduation_project_final.Controllers
{
    public class DoctorController : Controller
    {
        graduation_project db = new graduation_project();

        // GET: Doctor
        public ActionResult register_doctor()
        {
            if (TempData["user_id"] != null)
            {
                int id = (int)TempData["user_id"];
                Session.Add("id_user", id);
                user s = db.users.Where(n => n.id_user == id).FirstOrDefault();
                register_D u1 = new register_D()
                {
                    user_Doctor = s
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
                    register_D u1 = new register_D()
                    {
                        user_Doctor = s
                    };
                    return View(u1);

                }
                else
                {

                    return RedirectToAction("login", "Admin");
                }
            }
            //session.add("id_user", id);
            //user s = db.users.Where(n => n.id_user == id).FirstOrDefault();
            //Register_st u1 = new Register_st() {
            //    user_student= s };

        }
        [HttpPost]
        public ActionResult register_doctor(register_D doctor1, List<string> field_name, HttpPostedFileBase image)
        {
            user s = doctor1.user_Doctor;
            organization org1 = doctor1.user_organization_D;



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
            Staff s1 = new Staff();
            s1.id_organization = org1.id_organization;
            s1.job = s.role;
            //if (ModelState.IsValid)
            //{
            db.Staffs.Add(s1);
            db.SaveChanges();
            //}
            return RedirectToAction("home","doctor");
        }
        //public ActionResult requests()
        //{

        //    int id = (int)Session["id_user"];
        //    organization od1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
        //    Staff st1 = db.Staffs.Where(n => n.id_organization == od1.id_organization).FirstOrDefault();
        //    req_page requests_page = new req_page();
        //    int counter = db.Requests.ToList().Count;
        //    while (counter > 0)
        //    {
        //        Request r1 = db.Requests.Where(n => n.id_prof == st1.id_staff).FirstOrDefault();
        //        project p1 = db.projects.Where(n => n.id_project == r1.id_project).FirstOrDefault();



        //        List<Request> rr1 = new List<Request>();
        //        List<project> pp1 = new List<project>();
        //        List<Request> rr2 = new List<Request>();
        //        List<project> pp2 = new List<project>();
        //        List<Request> rr3 = new List<Request>();
        //        List<project> pp3 = new List<project>();
        //        List<Request> rr4 = new List<Request>();
        //        List<project> pp4 = new List<project>();

        //        //return the idea of doctor
        //        if (r1.id_company == null && p1.creator_id != null)
        //        {
        //            rr1 = db.Requests.Where(n => n.id_company == r1.id_company).ToList();
        //            pp1 = db.projects.Where(n => n.id_project == p1.id_project).ToList();
        //        }
        //        //return suggest idea to doctor
        //        else if (r1.id_company == null && p1.creator_id == null)
        //        {
        //            rr3 = db.Requests.Where(n => n.id_company == r1.id_company).ToList();
        //            pp3 = db.projects.Where(n => n.id_project == p1.id_project).ToList();
        //        }
        //        //return suggest idea to company
        //        else if (r1.id_company != null && p1.company_supervisor == null)
        //        {
        //            rr4 = db.Requests.Where(n => n.id_company == r1.id_company).ToList();
        //            pp4 = db.projects.Where(n => n.id_project == p1.id_project).ToList();
        //        }
        //        //return the idea of company
        //        else if (r1.id_company != null && p1.creator_id != null)
        //        {
        //            rr2 = db.Requests.Where(n => n.id_company == r1.id_company).ToList();
        //            pp2 = db.projects.Where(n => n.id_project == p1.id_project).ToList();
        //        }

        //        if (rr1 != null)
        //            requests_page.req_D=rr1;
        //        if (rr2 != null)
        //            requests_page.req_C = rr2;
        //        if (rr3 != null)
        //            requests_page.req_suggest_D = rr3;
        //        if (rr4 != null)
        //            requests_page.req_suggest_C = rr4;
        //        if (pp1 != null)
        //            requests_page.project_D = pp1;
        //        if (pp2 != null)
        //            requests_page.project_C = pp2;
        //        if (pp3 != null)
        //            requests_page.project_suggest_D = pp3;
        //        if (pp4 != null)
        //            requests_page.project_suggest_C = pp4;


        //        counter--;
        //    }




        //    return View(requests_page);
        //}
        public ActionResult requests()
        {

            int id = (int)Session["id_user"];
            organization od1 = db.organizations.Where(n => n.id_user == id).FirstOrDefault();
            Staff st1 = db.Staffs.Where(n => n.id_organization == od1.id_organization).FirstOrDefault();
            req_page requests_page = new req_page();
            //int counter = db.Requests.ToList().Count;

                List<Request> r_d = db.Requests.Where(n => n.id_prof == st1.id_staff && n.id_company==null).ToList();
                List<Request> r_c = db.Requests.Where(n => n.id_prof == st1.id_staff && n.id_company != null).ToList();
            foreach (var item in r_d)
            {
                List<project> pp1 = db.projects.Where(n => n.id_project == item.id_project && n.creator_id == null).ToList();

                if (pp1.Count != 0)
                {
                    requests_page.project_suggest_D = pp1;
                    foreach (var item2 in requests_page.project_suggest_D)
                    {
                        requests_page.req_suggest_D = db.Requests.Where(n => n.id_project == item2.id_project).ToList();
                    }
                }
                List<project> pp2 = db.projects.Where(n => n.id_project == item.id_project && n.creator_id != null).ToList();
                

                if (pp2.Count != 0)
                {
                    requests_page.project_D = pp2;

                    foreach (var item2 in requests_page.project_D)
                    {
                        requests_page.req_D = db.Requests.Where(n => n.id_project == item2.id_project).ToList();

                    }
                }


            }
            foreach (var item in r_c)
            {
                List<project>pp3= db.projects.Where(n => n.id_project == item.id_project && n.company_supervisor == null).ToList();
                if (pp3.Count != 0) 
                {
                    requests_page.project_suggest_C = pp3;
                    foreach (var item2 in requests_page.project_suggest_C)
                    {
                        requests_page.req_suggest_C = db.Requests.Where(n => n.id_project == item2.id_project).ToList();
                    }

                }
                List<project>pp4= db.projects.Where(n => n.id_project == item.id_project && n.company_supervisor != null).ToList();

                if (pp4.Count != 0)
                {
                    requests_page.project_C = pp4;

                    foreach (var item2 in requests_page.project_C)
                    {
                        requests_page.req_C = db.Requests.Where(n => n.id_project == item2.id_project).ToList();

                    }
                }


            }

            




            return View(requests_page);
        }
        //idea of doctor
        public ActionResult ideas()
        {
            int id = (int)Session["id_user"];
            ViewBag.ideas = db.projects.Where(n => n.creator_id == id).ToList();
            

            return View();
        }

        [HttpPost]
        public ActionResult add_idea(project p)
        {
            int id = (int)Session["id_user"];
            p.creator_id = id;
            if (ModelState.IsValid)
            {
                db.projects.Add(p);
                db.SaveChanges();
            }
            return RedirectToAction("ideas","Doctor");
        }
        public ActionResult edit_idea(int id)
        {
            project pp = db.projects.Where(n => n.id_project == id).FirstOrDefault();

            return View(pp);
        }
        [HttpPost]
        public ActionResult update_idea(project pp)
        {
            project p1 = db.projects.Where(n => n.id_project == pp.id_project).FirstOrDefault();
            p1.name = pp.name;
            p1.description = pp.description;
            db.SaveChanges();
            return RedirectToAction("ideas", "Doctor");

        }
        public ActionResult delete_idea(int id)
        {
            project p1 = db.projects.Where(n => n.id_project == id).FirstOrDefault();

            db.projects.Remove(p1);
            db.SaveChanges();
            return RedirectToAction("ideas", "Doctor");

        }


        public ActionResult profile()
        {
            if (Session["id_user"] != null)
            {
                int id = (int)Session["id_user"];
                user u1 = db.users.Where(n => n.id_user == id &&n.role=="doctor").FirstOrDefault();
                organization o1 = db.organizations.Where(n => n.id_user == u1.id_user).FirstOrDefault();
                string skills = o1.skills;
                string[] sk = skills.Split('-');
                ViewBag.skills = sk;

                profile_display p1 = new profile_display()
                {
                    user1 = u1,
                    organization1 = o1,

                };
                return View(p1);

            }
            return View();
        }
        public ActionResult edit_profile(int id)
        {
            user u1 = db.users.Where(n => n.id_user == id).FirstOrDefault();
            organization o1 = db.organizations.Where(n => n.id_user == u1.id_user).FirstOrDefault();
            string skills = o1.skills;
            string[] sk = skills.Split('-');
            ViewBag.skills = sk;

            profile_display p1 = new profile_display()
            {
                user1 = u1,
                organization1 = o1,
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
            u1.email = u1_m.email;
            o1.education = o1_m.education;

            //string skill = "";
            //    skill+= o1.skills;
            int len = field_name.Count;

            List<string> new_list = new List<string>();
            for (int i = 0; i < len; i++)
            {

                if (field_name[i] == "")
                {
                    continue;
                }
                new_list.Add(field_name[i]);


            }
            var skill = string.Join("-", new_list);
            o1.skills = skill;

            db.SaveChanges();

            return RedirectToAction("home");
        }
        public ActionResult notify_accept(int id)
        {

            Request r1 = db.Requests.Where(n => n.id_request == id).FirstOrDefault();
            r1.prof_accept = true;
            db.SaveChanges();

            //project p1 = db.projects.Where(n => n.id_project == r1.id_project).FirstOrDefault();

            //student s1 = db.students.Where(n => n.id_student == r1.id_student).FirstOrDefault();

            //organization o1 = db.organizations.Where(n => n.id_organization == s1.id_organization).FirstOrDefault();
            //user u1 = db.users.Where(n => n.id_user == o1.id_user).FirstOrDefault();
            //ViewBag.message = $"the idea of {p1.name} is accepted  that have code";
            //ViewBag.code = r1.group_code;
           
            
            //remove from table of request
            //db.Requests.Remove(r1);


            return RedirectToAction("home");
        }
        public ActionResult notify_reject(int id)
        {

            Request r1 = db.Requests.Where(n => n.id_request == id).FirstOrDefault();
            project p1 = db.projects.Where(n => n.id_project == r1.id_project).FirstOrDefault();

            student s1 = db.students.Where(n => n.id_student == r1.id_student).FirstOrDefault();

            organization o1 = db.organizations.Where(n => n.id_organization == s1.id_organization).FirstOrDefault();
            user u1 = db.users.Where(n => n.id_user == o1.id_user).FirstOrDefault();
            ViewBag.message = $"the idea of {p1.name} is rejected  that have code";
            ViewBag.code = r1.group_code;
            //remove from table of request
            return RedirectToAction("home", "Student");
        }




        public ActionResult home()
        {
            if (TempData["user_id"] != null)
            {
                int id = (int)TempData["user_id"];
                Session.Add("id_user", id);
                return View();
            }
            else
            {
                if (Session["id_user"] != null)
                {
                    return View();
                }

            }
            return RedirectToAction("login", "Admin");
        }
    }
    }
