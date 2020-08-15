using System;
using System.Collections.Generic;
using System.Text;

namespace ViseoTechnicalTest.Service.ServiceModels.Request
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
