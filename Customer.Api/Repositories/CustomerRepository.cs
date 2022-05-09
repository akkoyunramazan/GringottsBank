using System;
using System.Threading.Tasks;
using Customer.Api.Data;
using Customer.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CustomerEntity> _customerEntity;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _customerEntity = _dbContext.Set<CustomerEntity>();
        }
        
        public async Task<CustomerEntity> GetByCustomerNumber(int customerNumber)
        {
            return await _customerEntity.AsNoTracking()
                .FirstOrDefaultAsync(e => e.CustomerNumber == customerNumber);
        }

        public async Task<CustomerEntity> Create(CustomerEntity customerInformation)
        {
            var customer = await _customerEntity.AddAsync(customerInformation);
            await _dbContext.SaveChangesAsync();
            return customerInformation;
        }
        
    }
}