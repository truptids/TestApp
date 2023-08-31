using TestApplication.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Model.UserManagement.Responses
{
    public class ViewUserList
    {
      
        public List<UserManagerViewModel> Users { get; set; }
    }

    public class UserManagerViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
       
    }
}
