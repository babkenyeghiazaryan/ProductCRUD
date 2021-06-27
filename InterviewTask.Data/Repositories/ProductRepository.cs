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

        public IQueryable<Product> GetAll(Pagination pagination, Filter filter)
        {
            var usersQuery = dbContext.Products as IQueryable<Product>;

            if (filter.Id.HasValue)
                usersQuery = usersQuery.Where(item => item.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                usersQuery = usersQuery.Where(item => item.Name.ToLower().Contains(filter.Name.ToLower()));
            if (!string.IsNullOrWhiteSpace(filter.Description))
                usersQuery = usersQuery.Where(item => item.Description.ToLower().Contains(filter.Description.ToLower()));
            if (filter.PriceStarts.HasValue)
                usersQuery = usersQuery.Where(item => item.Price >= filter.PriceStarts);
            if (filter.PriceEnds.HasValue)
                usersQuery = usersQuery.Where(item => item.Price <= filter.PriceEnds);
            if (filter.Available.HasValue)
                usersQuery = usersQuery.Where(item => item.Available == filter.Available);
            if (filter.DateCreatedUTCFrom.HasValue)
                usersQuery = usersQuery.Where(item => item.DateCreatedUTC >= filter.DateCreatedUTCFrom);
            if (filter.DateCreatedUTCTo.HasValue)
                usersQuery = usersQuery.Where(item => item.DateCreatedUTC <= filter.DateCreatedUTCTo);

            if (pagination != null)
            {
                //INFO: OrderByDynamic is custom written dynamic order by expression
                pagination.Count = usersQuery.Count();
                if (pagination.Sort.Direction != null && !string.IsNullOrEmpty(pagination.Sort.Member))
                {
                    usersQuery = usersQuery.OrderByDynamic(pagination.Sort.Member, pagination.Sort.Direction.Value);
                }
                usersQuery = usersQuery.Skip(pagination.Page.Number * pagination.Page.Size).Take(pagination.Page.Size);
            }
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
