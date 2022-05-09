using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.Models;
using Transaction.Api.Services;
using Transaction.Api.Types;

namespace Transaction.Api.Controllers
{
    [Authorize]
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionModel accountTransactionModel)
        {
            var accountTransaction = _mapper.Map<AccountTransaction>(accountTransactionModel);
            accountTransaction.TransactionType = TransactionType.Deposit;
            accountTransaction.AccountNumber = accountTransactionModel.AccountNumber;
            accountTransaction.Amount = new Money(accountTransactionModel.Amount, Currency.TRY);
            
            var result = await _transactionService.Deposit(accountTransaction);
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(result));
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionModel accountTransactionModel)
        {
            var accountTransaction = _mapper.Map<AccountTransaction>(accountTransactionModel);
            accountTransaction.AccountNumber = accountTransactionModel.AccountNumber;
            accountTransaction.TransactionType = TransactionType.Withdrawal;
            accountTransaction.AccountNumber = accountTransactionModel.AccountNumber;
            accountTransaction.Amount = new Money(accountTransactionModel.Amount, Currency.TRY);
            var result = await _transactionService.Withdraw(accountTransaction);
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(result));
        }

        [HttpGet("byaccount")]
        public async Task<IActionResult> GetByAccount(int accountNumber)
        {
            var transactionList = await _transactionService.GetTransactionsByAccountNumber(accountNumber);
            return Ok(_mapper.Map<List<TransactionInquiryResult>>(transactionList));
        }
        
        [HttpGet("bycustomer")]
        public async Task<IActionResult> GetByCustomer(int customerNumber, DateTime startDate, DateTime endDate)
        {
            var transactionList = await _transactionService.GetTransactionsByCustomerNumber(customerNumber, startDate, endDate);
            return Ok(_mapper.Map<List<TransactionInquiryResult>>(transactionList));
        }
    }
}
