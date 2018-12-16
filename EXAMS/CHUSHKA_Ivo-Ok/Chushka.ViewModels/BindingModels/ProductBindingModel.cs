namespace Chushka.ViewModels.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductBindingModel
    {
        public int Id { get; set; }

        [MinLength(4)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ProductType { get; set; }
    }
}