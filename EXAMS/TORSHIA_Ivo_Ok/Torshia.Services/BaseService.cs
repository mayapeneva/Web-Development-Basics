namespace Torshia.Services
{
    using Data;

    public abstract class BaseService
    {
        protected BaseService(TorshiaDbContext db)
        {
            this.Db = db;
        }

        public TorshiaDbContext Db { get; set; }
    }
}