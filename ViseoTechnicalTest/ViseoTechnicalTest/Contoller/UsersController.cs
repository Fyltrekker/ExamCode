using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViseoTechnicalTest.RequestResponse.Request;
using ViseoTechnicalTest.RequestResponse.Response;
using ViseoTechnicalTest.Service;

namespace ViseoTechnicalTest.Contoller
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : IdentityBase
    {
        private IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        /// <summary>
        /// Get User info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var users = usersService.Read(new Service.ServiceModels.Request.ReadRequest { UserType = GetRole() });
            return Ok(new ReadResponse
            {
                Users = users.Users.Select(x => new User 
                {      
                    Email = x.Email,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Password = x.Password,
                    UserId = x.UserId,
                    Username = x.Username,
                    UserType = (RequestResponse.Enums.UserType)x.UserType,
                    Status = (RequestResponse.Enums.Status)x.Status,
                }).ToList()
            }); 
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {            
            var emailExist = usersService.EmailExists(request.Email);
            if (emailExist)
            {
                return BadRequest("Email already exist.");
            }
            else 
            {
                usersService.Create(new Service.ServiceModels.Request.CreateRequest 
                {
                    Email = request.Email,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Password = request.Password,             
                    Username = request.Username,
                    UserType = (Service.Enums.UserType)request.UserType,
                    Status = (Service.Enums.Status)request.Status
                });
                return Ok("User added successfully.");
            }
        }

        /// <summary>
        /// Soft deletes a certain user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = usersService.Delete(new Service.ServiceModels.Request.DeleteRequest { UserId = id });

            if (result)
            {
                return Ok("User has been successfully deleted.");
            }
            else
            {
                return BadRequest("Failed to delete.");
            } 
        }
    }
}
