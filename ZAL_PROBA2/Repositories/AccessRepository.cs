using Microsoft.EntityFrameworkCore;
using ZAL_PROBA2.EFContext;
using ZAL_PROBA2.Models;

namespace ZAL_PROBA2.Repositories;

public interface IAccessRepository
{
    public Task<Access?> GetByUserIdAndProject(int idUser, int idProject);
}

public class AccessRepository(SqlDbContext context) : IAccessRepository
{
    public async Task<Access?> GetByUserIdAndProject(int idUser, int idProject)
    {
        return await context.Accesses.FirstOrDefaultAsync(a => a.IdProject == idProject && a.IdUser == idUser);
    }
}