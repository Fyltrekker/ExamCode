using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViseoTechnicalTest.RequestResponse.Enums;

namespace ViseoTechnicalTest.RequestResponse.Request
{
    public class CreateUserRequest
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_-]){5}$", ErrorMessage = "Username should have the size of 5(Alphanumeric).")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Password should contain at least eight characters")]
        public string Password { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public Status Status { get; set; } = Status.Active;

    }
}
