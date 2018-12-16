namespace Chushka.Services
{
    using Data;

    public class BaseService
    {
        protected BaseService(ChushkaDbContext db)
        {
            this.Db = db;
        }

        public ChushkaDbContext Db { get; set; }
    }
}