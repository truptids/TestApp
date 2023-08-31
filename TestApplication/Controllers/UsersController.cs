using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using TestApplication.Model.Database.Entities;

using TestApplication.Model.UserManagement.Responses;
using TestApplication.Services;
using TestApplication.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TestApplication.Shared;

namespace TestApplication.Controllers
{
  
    [Route("api/users")]
    [ApiController]
    public class UsersController : RevControllerBase<IUserService>
    {
       
        public UsersController(IUserService service) : base(service)
        {
           
        }
        [HttpGet("GetAllUsers")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserManagerViewModel>))]
        public async Task<List<UserManagerViewModel>> GetAllUsers()
        {
            
            return await Service.GetAllUsers();

        }
       

      
      

       
    }
}
