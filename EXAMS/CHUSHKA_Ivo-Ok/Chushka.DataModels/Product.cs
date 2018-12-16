namespace Chushka.DataModels
{
    using Enum;

    public class Product : BaseModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }
    }
}