namespace PANDA.DataModels
{
    using System;
    using Enums;

    public class Package : BaseModel<int>
    {
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus PackageStatus { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public virtual User Recipient { get; set; }

        public int RecipientId { get; set; }
    }
}