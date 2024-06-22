using Microsoft.EntityFrameworkCore;
using ZAL_PROBA2.EFContext;
using ZAL_PROBA2.Models;

namespace ZAL_PROBA2.Repositories;

public interface IUserRepository
{
    public Task<User?> GetOneById(int idUser);
}

public class UserRepository(SqlDbContext context) : IUserRepository
{
    public async Task<User?> GetOneById(int idUser)
    {
        return await context.Users.FirstOrDefaultAsync(client => client.IdUser == idUser);
    }
}