using System;
using System.Collections.Generic;
using System.Text;
using ViseoTechnicalTest.Service.ServiceModels.Request;
using ViseoTechnicalTest.Service.ServiceModels.Response;

namespace ViseoTechnicalTest.Service
{
    public interface IUsersService
    {
        public ReadResponse Read(ReadRequest request);
        public bool Create(CreateRequest request);
        public bool Delete(DeleteRequest request);
        public bool EmailExists(string email);
        public AuthenticateResponse GenerateToken(AuthenticateRequest request);
    }
}
