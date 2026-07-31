using GymApp.DAL.Context;
using GymApp.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Repositories;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{

    public MemberRepository(GymDbContext context) : base(context)
    {
        
    }

    public Task<bool> AnyAsync(Expression<Func<Member, bool>> prediacate, CancellationToken cancellationToken = default)
        => _context.Members.AnyAsync(prediacate, cancellationToken);

    public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        => _context.Members.AnyAsync(m => m.Email == email, cancellationToken);

    public Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken = default)
        => _context.Members.AnyAsync(m => m.Phone == phone, cancellationToken);


}

