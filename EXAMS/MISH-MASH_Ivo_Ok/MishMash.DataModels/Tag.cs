namespace MishMash.DataModels
{
    using Base;

    public class Tag : BaseModel<int>
    {
        public string Name { get; set; }
    }
}