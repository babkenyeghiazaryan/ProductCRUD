using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.Data.Entities
{
    /// <summary>
    /// This interface can be implemented by those entitis which need to have the following fields.
    /// </summary>
    public interface IEntityBase
    {
        long Id { get; set; }
        DateTime DateCreatedUTC { get; set; }
    }
}
