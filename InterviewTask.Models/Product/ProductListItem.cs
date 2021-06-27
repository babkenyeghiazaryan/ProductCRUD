using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.Models.Product
{
    public class ProductItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
    }
}
