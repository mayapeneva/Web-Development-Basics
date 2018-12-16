namespace Chushka.DataModels
{
    using System;

    public class Order : BaseModel<int>
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual User Client { get; set; }
        public int ClientId { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}