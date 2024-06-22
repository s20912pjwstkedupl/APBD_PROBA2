using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ZAL_PROBA2.Dtos;
using ZAL_PROBA2.EFContext;
using Task = ZAL_PROBA2.Models.Task;

namespace ZAL_PROBA2.Repositories;

public interface ITaskRepository
{
    public Task<List<GetTasksDto>> GetForProject(int idProject);
    public Task<Task> Add(Task task);
}
public class TaskRepository(SqlDbContext _context) : ITaskRepository
{
    public async Task<List<GetTasksDto>> GetForProject(int idProject)
    {
        return await _context.Tasks.Where(p => p.IdProject == idProject).Select(t => new GetTasksDto()
        {
            IdTask = t.IdTask,
            Name = t.Name,
            Description = t.Description,
            CreatedAt = t.CreatedAt,
            IdProject = t.IdProject,
            IdReporter = t.IdReporter,
            Reporter = new GetUserTaskDto()
            {
                FirstName = t.VReporter.FirstName,
                LastName = t.VReporter.LastName,
            },
            IdAssignee = t.IdAssignee,
            Assignee = new GetUserTaskDto()
            {
                FirstName = t.VAssignee.FirstName,
                LastName = t.VAssignee.LastName,
            },
        }).ToListAsync();
    }
    
    public async Task<Task> Add(Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }
}