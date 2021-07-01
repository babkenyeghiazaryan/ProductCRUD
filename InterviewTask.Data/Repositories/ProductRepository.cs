using InterviewTask.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using InterviewTask.Data.Extensions;
using InterviewTask.Data.Paging;
using InterviewTask.Models.Filtering;

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

        public void Remove(Product product, byte[] RowVersion)
        {
            var entry = dbContext.Entry(product).Property(u => u.RowVersion);
            dbContext.Remove(product);

            dbContext.Entry(product).State = EntityState.Deleted;
            
        }

        public async Task<Product> Get(double id)
        {
            return await dbContext.Products.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<Product> Get(double[] ids)
        {
            var productsQuery = dbContext.Products.Where(u => ids.Contains(u.Id));

            return productsQuery;
        }

        public IQueryable<Product> GetAll(Pagination pagination, Filter filter)
        {
            var productsQuery = dbContext.Products as IQueryable<Product>;

            if (filter.Id.HasValue && filter.Id!=0)
                productsQuery = productsQuery.Where(item => item.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                productsQuery = productsQuery.Where(item => item.Name.ToLower().Contains(filter.Name.ToLower()));
            if (!string.IsNullOrWhiteSpace(filter.Description))
                productsQuery = productsQuery.Where(item => item.Description.ToLower().Contains(filter.Description.ToLower()));
            if (filter.PriceStarts.HasValue)
                productsQuery = productsQuery.Where(item => item.Price >= filter.PriceStarts);
            if (filter.PriceEnds.HasValue)
                productsQuery = productsQuery.Where(item => item.Price <= filter.PriceEnds);
            if (filter.Available.HasValue)
                productsQuery = productsQuery.Where(item => item.Available == filter.Available);
            if (filter.DateCreatedUTCFrom.HasValue)
                productsQuery = productsQuery.Where(item => item.DateCreatedUTC >= filter.DateCreatedUTCFrom);
            if (filter.DateCreatedUTCTo.HasValue)
                productsQuery = productsQuery.Where(item => item.DateCreatedUTC <= filter.DateCreatedUTCTo);

            if (pagination != null)
            {
                //INFO: OrderByDynamic is custom written dynamic order by expression
                pagination.Count = productsQuery.Count();
                if (pagination.Sort.Direction != null && !string.IsNullOrEmpty(pagination.Sort.Member))
                {
                    productsQuery = productsQuery.OrderByDynamic(pagination.Sort.Member, pagination.Sort.Direction.Value);
                }
                productsQuery = productsQuery.Skip(pagination.Page.Number * pagination.Page.Size).Take(pagination.Page.Size);
            }
            return productsQuery;
        }

        public void RemoveRange(IEnumerable<Product> products)
        {
            dbContext.RemoveRange(products);
        }

        public void Update(Product product, byte[] RowVersion)
        {
            var entry  = dbContext.Entry(product).Property(u => u.RowVersion);
            dbContext.Update(product);
            dbContext.Entry(product).State = EntityState.Modified;
           
        }

        public void UpdateRange(IEnumerable<Product> entities)
        {
            dbContext.UpdateRange(entities);
        }


    }
}
