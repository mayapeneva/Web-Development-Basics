namespace MyExam.DataModels
{
    using Base;
    using Enums;

    public class Order : BaseModel<int>
    {
        public OrderStatus Status { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual User Cashier { get; set; }
        public int CashierId { get; set; }
    }
}