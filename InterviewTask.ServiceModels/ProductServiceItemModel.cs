using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.ServiceModels
{
    public class ProductServiceItemModel
    {
            public long Id { get; set; }
            public string Name { get; set; }
            public bool IsAvailable { get; set; }
            public decimal Price { get; set; }
    }
}
