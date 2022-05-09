using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Transaction.Api.Data.Entities;
using Transaction.Api.Exceptions;
using Transaction.Api.Mappers;
using Transaction.Api.Models;
using Transaction.Api.Repositories;
using Transaction.Api.Services;
using Xunit;

namespace Transaction.Api.Test.Services
{
    public class TransactionServiceTest
    {
        protected TransactionService TransactionServiceUnderTest { get; }
        protected Mock<IAccountSummaryRepository> AccountSummaryRepositoryMock { get; }
        protected Mock<IAccountTransactionRepository> AccountTransactionRepositoryMock { get; }
        protected Mock<ILogger<TransactionService>> LoggerMock { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper Mapper { get; }
        
        public TransactionServiceTest()
        {
            AccountSummaryRepositoryMock = new Mock<IAccountSummaryRepository>();
            AccountTransactionRepositoryMock = new Mock<IAccountTransactionRepository>();
            LoggerMock = new Mock<ILogger<TransactionService>>();
            MappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = MappingConfig.CreateMapper();
            TransactionServiceUnderTest = new TransactionService(AccountSummaryRepositoryMock.Object, AccountTransactionRepositoryMock.Object, Mapper, LoggerMock.Object);
        }
        
        public class Deposit : TransactionServiceTest
        {
            [Fact]
            public void Should_return_transactionresult_on_successfulldeposit()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 12345678,
                    CustomerNumber = 87654321,
                    Balance = 55000,
                    Currency = "TRY"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 12345678,
                    TransactionType = Types.TransactionType.Deposit,
                    Amount = new Types.Money(1000, Types.Currency.TRY)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Deposit(accountTransaction).Result;

                // Assert
                Assert.Equal(accountSummary.Balance, result.Balance.Amount);
            }

            [Fact]
            public void Should_throw_transactionexception__when_transactiondetails_are_invalid()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 12345678,
                    CustomerNumber = 87654321,
                    Balance = 55000,
                    Currency = "TRY"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 12345678,
                    TransactionType = Types.TransactionType.Deposit,
                    Amount = new Types.Money(0, Types.Currency.TRY)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Deposit(accountTransaction);

                // Assert
                Assert.ThrowsAsync<InvalidAmountException>(async () => await result);
            }
        }

        public class Withdraw : TransactionServiceTest
        {
            [Fact]
            public void Should_return_transactionresult_on_successfullwithdraw()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 12345678,
                    CustomerNumber = 87654321,
                    Balance = 55000,
                    Currency = "TRY"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 12345678,
                    TransactionType = Types.TransactionType.Withdrawal,
                    Amount = new Types.Money(1000, Types.Currency.TRY)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Withdraw(accountTransaction).Result;

                // Assert
                Assert.Equal(accountSummary.Balance, result.Balance.Amount);
            }

            [Fact]
            public void Should_throw_transactionexception__when_transactiondetails_are_invalid()
            {
                // Arrange
                var accountSummary = new AccountSummaryEntity()
                {
                    AccountNumber = 12345678,
                    CustomerNumber = 87654321,
                    Balance = 55000,
                    Currency = "TRY"
                };

                var accountTransaction = new AccountTransaction()
                {
                    AccountNumber = 12345678,
                    TransactionType = Types.TransactionType.Withdrawal,
                    Amount = new Types.Money(65000, Types.Currency.TRY)
                };

                AccountTransactionRepositoryMock
                    .Setup(i => i.Create(It.IsAny<AccountTransactionEntity>(), It.IsAny<AccountSummaryEntity>()))
                    .Returns(Task.CompletedTask);

                AccountSummaryRepositoryMock
                    .Setup(i => i.Read(It.IsAny<int>()))
                    .ReturnsAsync(accountSummary);

                // Act
                var result = TransactionServiceUnderTest.Withdraw(accountTransaction);

                // Assert
                Assert.ThrowsAsync<InsufficientBalanceException>(async () => await result);
            }
        }
    }
}