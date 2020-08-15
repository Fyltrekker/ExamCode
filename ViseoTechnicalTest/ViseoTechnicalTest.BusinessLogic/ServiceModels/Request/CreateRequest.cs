using System;
using System.Collections.Generic;
using System.Text;
using ViseoTechnicalTest.Service.Enums;

namespace ViseoTechnicalTest.Service.ServiceModels.Request
{
    public class CreateRequest
    {       
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }     
        public UserType UserType { get; set; }      
        public Status Status { get; set; }
    }
}
