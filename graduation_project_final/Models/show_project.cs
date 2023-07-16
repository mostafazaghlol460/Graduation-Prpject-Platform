using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace graduation_project_final.Models
{
    public class show_project
    {
        public List<project> project_doctor { get; set; }
        public List<project> project_company { get; set; }
        public Request req1 { get; set; }
        public project req_project_student { get; set; }


    }
}