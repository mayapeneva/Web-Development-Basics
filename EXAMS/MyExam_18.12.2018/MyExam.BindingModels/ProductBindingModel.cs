namespace MyExam.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductBindingModel
    {
        [Required]
        public string Name { get; set; }

        public string Picture { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [RegularExpression(@"\d{12}")]
        public string Barcode { get; set; }
    }
}