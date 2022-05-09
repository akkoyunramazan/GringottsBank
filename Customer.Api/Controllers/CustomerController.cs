using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Api.Models;
using Customer.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [Authorize]
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int customerNumber)
        {
            var customerResult = await _customerService.GetCustomer(customerNumber);
            return Ok(_mapper.Map<CustomerResultModel>(customerResult));
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerModel customerInformationModel)
        {
            var customerResult = await _customerService.CreateCustomer(customerInformationModel);
            return Ok(_mapper.Map<CustomerResultModel>(customerResult));
        }
        
    }
}
