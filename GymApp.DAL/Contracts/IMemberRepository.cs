using System.Linq.Expressions;

namespace GymApp.DAL.Contracts;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> PhoneExistsAsync(string email, CancellationToken cancellationToken = default);

}
