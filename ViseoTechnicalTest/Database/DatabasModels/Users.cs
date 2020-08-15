using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ViseoTechnicalTest.Database.Enums;

namespace ViseoTechnicalTest.Database.DatabasModels
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [MaxLength(5)]
        public string Username { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        [Column("UserType")]
        public UserEnum UserType { get; set; }
        [Column("Status")]
        public StatusEnum Status { get; set; }
    }
    public class UsersMap : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.HasIndex(e => e.Email).IsUnique();
            CreateSeed(entity);
        }
        public void CreateSeed(EntityTypeBuilder<Users> entity)
        {
            entity.HasData(
                new Users 
                { 
                    UserId = 1,
                    Email = "admin@admin.com",
                    Password = "Password1",
                    Username = "Admin",
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Status = StatusEnum.Active,
                    UserType = UserEnum.Admin
                });
        }
    }
}
