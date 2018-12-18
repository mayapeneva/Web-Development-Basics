namespace MyExam.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class OrderBindingModel
    {
        [Required]
        [RegularExpression(@"\d{12}")]
        public string Barcode { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}