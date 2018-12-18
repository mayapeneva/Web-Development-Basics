namespace MyExam.DataModels
{
    using Base;

    public class Product : BaseModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Barcode { get; set; }

        public string Picture { get; set; }
    }
}