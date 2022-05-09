using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Api.Entities;
using Account.Api.Models;
using Account.Api.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Account.Api.Services
{
    public class AccountService : IAccountService
    {
        
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public AccountService(IAccountRepository accountRepository, IMapper mapper, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        
        public async Task<AccountResult> CreateAccount(AccountModel accountInformation)
        {
            var accountEntity = new AccountSummaryEntity();
            var rnd = new Random();
            accountEntity.AccountNumber = rnd.Next(1, 999999999);
            accountEntity.Currency = accountInformation.Currency;
            accountEntity.Balance = accountInformation.Balance;
            accountEntity.CustomerNumber = accountInformation.CustomerNumber;
            
            var account = await _accountRepository.Create(accountEntity);
            return _mapper.Map<AccountResult>(account);
        }

        public async Task<AccountResult> GetAccountByAccountNumber(int accountNumber)
        {
            var account = await _accountRepository.Read(accountNumber);
            return _mapper.Map<AccountResult>(account);
        }

        public async Task<List<AccountResult>> ReadAllAccountByCustomer(int customerNumber)
        {
            var accountList = await _accountRepository.ReadAllAccountByCustomer(customerNumber);
            return _mapper.Map<List<AccountResult>>(accountList);
        }
    }
}