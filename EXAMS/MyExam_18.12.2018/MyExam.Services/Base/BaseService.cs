namespace MyExam.Services.Base
{
    public abstract class BaseService
    {
        protected BaseService(MyExamDbContext context)
        {
            this.Db = context;
        }

        public MyExamDbContext Db { get; set; }
    }
}