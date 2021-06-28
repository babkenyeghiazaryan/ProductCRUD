using InterviewTask.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTask.Models
{
    public class ProductDetails
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool Available { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateCreatedUTC { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
