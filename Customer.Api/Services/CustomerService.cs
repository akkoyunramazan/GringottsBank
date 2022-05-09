using System;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Api.Entities;
using Customer.Api.Models;
using Customer.Api.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Customer.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomerResult> GetCustomer(int customerNumber)
        {
            _logger.LogInformation(customerNumber, "Customer inquiry with:{0}", customerNumber);

            var customer = await _customerRepository.GetByCustomerNumber(customerNumber);
            
            _logger.LogInformation(customerNumber, "Customer inquiry result:{0}", JsonConvert.SerializeObject(customer));

            return _mapper.Map<CustomerResult>(customer);
        }
        
        public async Task<CustomerResult> CreateCustomer(CustomerModel customerInformation)
        {
            var customerEntity = new CustomerEntity();
            var rnd = new Random();
            customerEntity.CustomerNumber = rnd.Next(1, 999999999);
            customerEntity.Name = customerInformation.Name;
            customerEntity.Surname = customerInformation.Surname;
            customerEntity.Username = customerInformation.Username;
            customerEntity.Password = customerInformation.Password;
            customerEntity.Date = DateTime.Now;

            var customer = await _customerRepository.Create(customerEntity);
            return _mapper.Map<CustomerResult>(customer);
        }
        
    }
}