using InterviewTask.Models.Filtering;
using InterviewTask.Models.Paging;
using InterviewTask.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.Models.ProductResponse
{
    public class ProductListResponsePaging
    {
            public IEnumerable<ProductItem> products{ get; set; }
            public PagingModel paging { get; set; }
            public Filter filtering { get; set; }
    }
}
