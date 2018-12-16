namespace MishMash.Services.Base
{
    using Data;

    public abstract class BaseService
    {
        protected BaseService(MishMashDbContext db)
        {
            this.Db = db;
        }

        public MishMashDbContext Db { get; set; }
    }
}