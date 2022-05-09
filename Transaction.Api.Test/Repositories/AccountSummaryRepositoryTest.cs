using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transaction.Api.Data;
using Transaction.Api.Data.Entities;
using Transaction.Api.Mappers;
using Transaction.Api.Repositories;
using Xunit;

namespace Transaction.Api.Test.Repositories
{
    public class AccountSummaryRepositoryTest
    {
        protected AccountSummaryRepository AccountSummaryRepositoryUnderTest { get; set; }
        protected ApplicationDbContext DbContextInMemory { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper Mapper { get; }

        public AccountSummaryRepositoryTest()
        {
            DbContextInMemory = GetInMemoryDbContext();
            MappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = MappingConfig.CreateMapper();
            AccountSummaryRepositoryUnderTest = new AccountSummaryRepository(DbContextInMemory);
        }

        public class Read : AccountSummaryRepositoryTest
        {
            [Fact]
            public void Should_return_accountsummary_when_accountnumber_exist()
            {
                // Arrange
                int existingAccountNumber = 99427802;

                // Act
                var result = AccountSummaryRepositoryUnderTest.Read(existingAccountNumber).Result;

                // Assert
                Assert.Equal(AccountSummaryDataEntity.AccountNumber, result.AccountNumber);
                Assert.Equal(AccountSummaryDataEntity.Balance, result.Balance);
                Assert.Equal(AccountSummaryDataEntity.Currency, result.Currency);
            }

            [Fact]
            public void Should_return_null_when_accountnumber_doesnotexist()
            {
                // Arrange
                int invalidAccountNumber = 9942780;
                AccountSummaryEntity expectedResult = null;

                // Act
                var actualResult = AccountSummaryRepositoryUnderTest.Read(invalidAccountNumber).Result;

                // Assert
                Assert.Equal(expectedResult, actualResult);
            }
        }

        private static ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase("gringottbank")
                      .Options;
            var context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var accountSummaryDataEntity = AccountSummaryDataEntity;
            context.Add(accountSummaryDataEntity);
            context.SaveChanges();

            return context;
        }

        protected static AccountSummaryEntity AccountSummaryDataEntity => new AccountSummaryEntity()
        {
            AccountNumber = 99427802,
            Balance = 255000,
            Currency = "TRY"
        };
    }  
}