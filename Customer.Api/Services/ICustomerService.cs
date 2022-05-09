using System.Threading.Tasks;
using Customer.Api.Models;

namespace Customer.Api.Services
{
    public interface ICustomerService
    {
        Task<CustomerResult> GetCustomer(int customerNumber);
        Task<CustomerResult> CreateCustomer(CustomerModel customerInformation);
    }
}