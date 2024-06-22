using Microsoft.EntityFrameworkCore;
using ZAL_PROBA2.EFContext;
using ZAL_PROBA2.Models;

namespace ZAL_PROBA2.Repositories;

public interface IProjectRepository
{
    public Task<Project?> GetOneById(int idProject);
}

public class ProjectRepository(SqlDbContext context) : IProjectRepository
{
    public async Task<Project?> GetOneById(int idProject)
    {
        return await context.Projects.FirstOrDefaultAsync(client => client.IdProject == idProject);
    }
}