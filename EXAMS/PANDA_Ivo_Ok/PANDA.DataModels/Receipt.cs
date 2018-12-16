namespace PANDA.DataModels
{
    using System;

    public class Receipt : BaseModel<int>
    {
        public decimal Fee { get; set; }

        public DateTime? IssuedOn { get; set; }

        public virtual User Recipient { get; set; }
        public int RecipientId { get; set; }

        public virtual Package Package { get; set; }
        public int PackageId { get; set; }
    }
}