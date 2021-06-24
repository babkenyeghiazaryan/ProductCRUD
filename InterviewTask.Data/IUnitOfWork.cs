using InterviewTask.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Data
{
    public interface IUnitOfWork
    {
        ProductRepository ProductRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
