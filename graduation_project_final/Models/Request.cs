namespace graduation_project_final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Request
    {
        [Key]
        
        public int id_request { get; set; }

        public int? id_student { get; set; }

        public int? id_prof { get; set; }

        public int? eng_id { get; set; }

        public int? id_company { get; set; }

        public int? id_project { get; set; }

        [StringLength(50)]
        public string group_code { get; set; }

        public bool prof_accept { get; set; }

        public bool eng_accept { get; set; }

        public bool company_accept { get; set; }

        public virtual company company { get; set; }

        public virtual Group Group { get; set; }

        public virtual project project { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual student student { get; set; }
    }
}
