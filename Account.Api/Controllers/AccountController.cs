using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Api.Models;
using Account.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        
        [HttpGet("byaccount")]
        public async Task<IActionResult> GetAccountInformation(int accountNumber)
        {
            var accountResult = await _accountService.GetAccountByAccountNumber(accountNumber);
            return Ok(_mapper.Map<AccountResultModel>(accountResult));
        }

        [HttpGet("bycustomer")]
        public async Task<IActionResult> GetAccountListByCustomer(int customerNumber)
        {
            var accountList = await _accountService.ReadAllAccountByCustomer(customerNumber);
            return Ok(_mapper.Map<List<AccountResultModel>>(accountList));
        }

        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] AccountModel accountInformationModel)
        {
            var accountResult = await _accountService.CreateAccount(accountInformationModel);
            return Ok(_mapper.Map<AccountResultModel>(accountResult));
        }
    }
}
