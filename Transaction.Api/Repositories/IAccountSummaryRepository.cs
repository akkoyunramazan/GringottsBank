using System.Threading.Tasks;
using Transaction.Api.Data.Entities;

namespace Transaction.Api.Repositories
{
    public interface IAccountSummaryRepository
    {
        Task<AccountSummaryEntity> Read(int accountNumber);
    }
}