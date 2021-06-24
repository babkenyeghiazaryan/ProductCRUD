using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Data.Repositories
{
    interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(double id);
        IQueryable<TEntity> Get(double[] ids);
        IQueryable<TEntity> GetAll();
        Task<TEntity> Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
