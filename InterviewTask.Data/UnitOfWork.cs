using InterviewTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext context;
        private ProductRepository productRepository;

        public UnitOfWork(ApplicationDBContext context)
        {
            this.context = context;
        }

        public ProductRepository ProductRepository
        {
            get { return productRepository ?? (productRepository = new ProductRepository(context)); }
        }
        #region Save
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                DbUpdateExceptionHandler(ex);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DbUpdateExceptionHandler(ex);
            }
        }

        private void DbUpdateExceptionHandler(DbUpdateException ex)
        {
            var builder = new StringBuilder();

            foreach (var result in ex.Entries)
            {
                builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
            }

            throw new Exception(builder.ToString(), ex);
        }

        #endregion

        #region Diposable   

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
