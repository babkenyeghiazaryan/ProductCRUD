using InterviewTask.Models.Filtering;
using InterviewTask.Models.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.Models.ProductRequest
{
    //public class UserPagingResponseModel
    //{
    //    public IEnumerable<UserModel> users { get; set; }
    //    public PagingModel paging { get; set; }
    //    public Filter filtering { get; set; }
    //}
    public class ProductRequestModel
    {
        public PagingModel paging { get; set; }
        public Filter filter { get; set; }
    }
}
