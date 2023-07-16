using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace graduation_project_final.Models
{
    public partial class graduation_project : DbContext
    {
        public graduation_project()
            : base("name=graduation_project")
        {
        }

        public virtual DbSet<company> companies { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<organization> organizations { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Staff_Project> Staff_Project { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<company>()
                .HasMany(e => e.projects)
                .WithOptional(e => e.company)
                .HasForeignKey(e => e.company_supervisor);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Requests)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.group_code);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.students)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.group_code);

            modelBuilder.Entity<project>()
                .HasMany(e => e.students)
                .WithOptional(e => e.project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Requests)
                .WithOptional(e => e.Staff)
                .HasForeignKey(e => e.id_prof);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Staff_Project)
                .WithOptional(e => e.Staff)
                .HasForeignKey(e => e.staff_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.projects)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.creator_id);
        }
    }
}
