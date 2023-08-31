using TestApplication.Repository.Application.Interfaces;

namespace TestApplication.Shared
{
    public abstract class ServiceBase<TRepo>
    {
        protected TRepo Repository { get; }
        
        protected ServiceBase(TRepo repository)
        {
            Repository = repository;
        }

       
    }
}
