namespace graduation_project_final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_Project
    {
        [Key]
        public int id_staff_project { get; set; }

        public int? id_project { get; set; }

        public int? staff_id { get; set; }

        public virtual project project { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
