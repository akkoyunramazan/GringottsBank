using System.Threading.Tasks;
using Customer.Api.Entities;
using Customer.Api.Models;

namespace Customer.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetByCustomerNumber(int customerNumber);
        Task<CustomerEntity> Create(CustomerEntity customerInformation);
    }
}