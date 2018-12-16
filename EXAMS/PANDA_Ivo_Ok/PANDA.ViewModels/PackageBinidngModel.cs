namespace PANDA.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PackageBinidngModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string Recipient { get; set; }
    }
}