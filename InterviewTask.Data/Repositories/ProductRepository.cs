using InterviewTask.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using InterviewTask.Data.Extensions;


namespace InterviewTask.Data.Repositories
{
    public class ProductRepository : RepositoryBase, IRepository<Product>, IRangeRepository<Product>
    {
        public ProductRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public async Task AddRangeAsync(IEnumerable<Product> entities)
        {
            await dbContext.AddRangeAsync(entities);
        }

        public async Task<Product> Add(Product item)
        {
            await dbContext.AddAsync(item);
            return item;
        }

        public void Remove(Product user)
        {
            dbContext.Remove(user);
        }

        public async Task<Product> Get(double id)
        {
            return await dbContext.Products.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<Product> Get(double[] ids)
        {
            var usersQuery = dbContext.Products.Where(u => ids.Contains(u.Id));

            return usersQuery;
        }

        public IQueryable<Product> GetAll()
        {
            var usersQuery = dbContext.Products as IQueryable<Product>;

            return usersQuery;
        }

        public void RemoveRange(IEnumerable<Product> users)
        {
            dbContext.RemoveRange(users);
        }

        public void Update(Product user)
        {
            dbContext.Update(user);//Entry(user).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<Product> entities)
        {
            dbContext.UpdateRange(entities);
        }

        
    }
}
