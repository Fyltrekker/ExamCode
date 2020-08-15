using System;
using System.Collections.Generic;
using System.Text;

namespace ViseoTechnicalTest.Jwt
{
    public interface IJwTokenizer
    {
        public string CreateToken(string username, List<string> roles, DateTime tokenExpiration);
    }
}
