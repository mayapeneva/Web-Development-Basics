﻿namespace Chushka.ViewModels.ViewModels
{
    using DataModels.Enum;

    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
    }
}