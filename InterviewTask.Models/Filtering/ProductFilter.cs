using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.Models.Filtering
{
    public class Filter
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public decimal? PriceStarts { get; set; }
        public decimal? PriceEnds { get; set; }
        public bool? Available { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreatedUTCFrom { get; set; }
        public DateTime? DateCreatedUTCTo { get; set; }
    }
}
