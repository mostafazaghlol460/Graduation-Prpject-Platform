using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace graduation_project_final.Models
{
    public class req_page
    {
        public List<Request> req_D { get; set; }
        public List<Request> req_C { get; set; }
        public List<Request> req_suggest_D { get; set; }
        public List<Request> req_suggest_C { get; set; }


        public List<project> project_D { get; set; }
        public List<project> project_C { get; set; }
        public List<project> project_suggest_D { get; set; }
        public List<project> project_suggest_C { get; set; }


    }
}