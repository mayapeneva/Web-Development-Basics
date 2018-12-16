namespace MyExam.Services.Base
{
    using Data;

    public abstract class BaseService
    {
        protected BaseService(MyExamDbContext context)
        {
            this.Db = context;
        }

        public MyExamDbContext Db { get; set; }
    }
}