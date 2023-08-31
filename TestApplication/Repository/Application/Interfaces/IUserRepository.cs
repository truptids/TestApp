using System.Collections.Generic;
using System.Threading.Tasks;

using TestApplication.Model.Database.Entities;

using TestApplication.Model.UserManagement.Responses;
using TestApplication.Shared.Repositories;

namespace TestApplication.Repository.Application.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
       
        Task<List<UserManagerViewModel>> GetAllUsers();
       
        

    }
}