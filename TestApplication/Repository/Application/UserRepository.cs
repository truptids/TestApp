using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using TestApplication.Model.Base;
using TestApplication.Model.Database;
using TestApplication.Model.Database.Entities;

using TestApplication.Model.UserManagement.Responses;
using TestApplication.Repository.Application.Interfaces;
using TestApplication.Shared;
using TestApplication.Shared.Repositories;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using User = TestApplication.Model.Database.Entities.User;

namespace TestApplication.Repository.Application
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public readonly IConfiguration _configuration;

        public UserRepository(UserContext dbDbContext, IConfiguration configuration) : base(dbDbContext)
        {
            _configuration = configuration;
        }

        public async Task<List<UserManagerViewModel>> GetAllUsers()
        {

            List<UserManagerViewModel> result = new List<UserManagerViewModel>();

            string Command = UserResource.UserRoleQuery.ToString(); 
            
            string ConnectionString = _configuration.GetConnectionString("DbConnection").ToString();
        
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(Command, myConnection))
                {
                    SqlDataAdapter adp = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    adp.SelectCommand = myCommand;
                    adp.Fill(ds);
                    result = ds.Tables[0].AsEnumerable().Select(dr => new UserManagerViewModel
                    {
                        Id = Convert.ToInt32(dr[0]),
                        FullName = dr[1].ToString(),
                        Role = dr[2].ToString()
                    }).ToList();
                    myConnection.Close();
                }
            }
            return result;
        }
        

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return base.BeginTransactionAsync(cancellationToken);
        }

        protected override Expression<Func<User, int>> Key => user => user.Id;
        protected override DbSet<User> DbSet => DbContext.User;
        protected override string DbSetDisplayName => "User";
        protected override Expression<Func<User, object>> DefaultOrderBy => user => user.FullName;

    }
}
