using InterviewTask.Data;
using InterviewTask.Data.Entities;
using InterviewTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }

      
    }
}
