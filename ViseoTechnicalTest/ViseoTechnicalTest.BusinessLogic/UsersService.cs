using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViseoTechnicalTest.Database;
using ViseoTechnicalTest.Database.DatabasModels;
using ViseoTechnicalTest.Jwt;
using ViseoTechnicalTest.Service.ServiceModels.Request;
using ViseoTechnicalTest.Service.ServiceModels.Response;

namespace ViseoTechnicalTest.Service
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext applicationDbContext;
        private IJwTokenizer jwTokenizer;
        public UsersService(ApplicationDbContext applicationDbContext, IJwTokenizer jwTokenizer) 
        {
            this.applicationDbContext = applicationDbContext;
            this.jwTokenizer = jwTokenizer;
        }
        public bool EmailExists(string email)
        {
            var result = applicationDbContext.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower())  == null ? false: true;
            return result;
        }
        public bool Create(CreateRequest request)
        {
            try
            {
                var result = applicationDbContext.Users.Add(new Database.DatabasModels.Users
                {
                    Email = request.Email,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Password = request.Password,
                    Username = request.Username,
                    Status = (Database.Enums.StatusEnum)request.Status,
                    UserType = (Database.Enums.UserEnum)request.UserType
                });
                applicationDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool Delete(DeleteRequest request)
        {
            var success = false;
            var user = applicationDbContext.Users.FirstOrDefault(x => x.UserId == request.UserId && x.Status == Database.Enums.StatusEnum.Active);
            if (user != null)
            {
                //As per requirements Apply soft delete having status set to "0"
                //in the Primary Information Deleted[3]
                user.Status = Database.Enums.StatusEnum.Deleted;
                applicationDbContext.SaveChanges();
                success = true;
            }
            
            return success;            
        }

        public ReadResponse Read(ReadRequest readRequest)
        {
            var result = readRequest.UserType == Enums.UserType.Admin ? GetAllUsers : GetNonAdminUsers;
            var users = result.Select(x => 
                new ServiceModels.Response.User
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Password = x.Password,
                    Username = x.Username,
                    Status = (Enums.Status)x.Status,
                    UserType = (Enums.UserType)x.UserType,
                }).ToList();
            return new ReadResponse { Users = users };
        }

        public AuthenticateResponse GenerateToken(AuthenticateRequest request)
        {
            var token = "";
            var user = applicationDbContext.Users.FirstOrDefault(x => x.Username == request.Username 
                && x.Password == request.Password 
                && x.Status == Database.Enums.StatusEnum.Active);
            if (user != null)
            {
                string role = Enum.GetName(typeof(Database.Enums.UserEnum), user.UserType);
                token = jwTokenizer.CreateToken(user.Username, new List<string> { role }, DateTime.UtcNow.AddHours(1));
            }
            return new AuthenticateResponse {  JWT = token };
          
        }

        //Don't return deleted data. Safe to say return non deleted data, active and inactive
        private List<Database.DatabasModels.Users> GetAllUsers => applicationDbContext.Users.Where(x=>x.Status != Database.Enums.StatusEnum.Deleted).ToList();
        private List<Database.DatabasModels.Users> GetNonAdminUsers => applicationDbContext.Users.Where(x=>x.UserType == Database.Enums.UserEnum.User && x.Status != Database.Enums.StatusEnum.Deleted).ToList();
    }
}
