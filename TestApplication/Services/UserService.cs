using TestApplication.Model.UserManagement.Responses;
using TestApplication.Repository.Application.Interfaces;
using TestApplication.Repository.Application;
using System.Collections.Generic;
using TestApplication.Shared;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = System.IO.File;
using System.Collections.Generic;

namespace TestApplication.Services

{
    public interface IUserService
    {
        

        Task<List<UserManagerViewModel>> GetAllUsers();
    }

    public class UserService : ServiceBase<IUserRepository>,IUserService
    {
       public UserService(IUserRepository repository ) : base(repository)
        {
        }

      
        public async Task<List<UserManagerViewModel>> GetAllUsers()
        {
            return await Repository.GetAllUsers();
        }

       
    }
}