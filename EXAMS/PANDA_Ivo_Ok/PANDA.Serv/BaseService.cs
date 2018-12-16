namespace PANDA.Serv
{
    using Data;

    public abstract class BaseService
    {
        protected BaseService(PandaDbContext db)
        {
            this.Db = db;
        }

        public PandaDbContext Db { get; set; }
    }
}