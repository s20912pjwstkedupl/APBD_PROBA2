using ZAL_PROBA2.Dtos;
using ZAL_PROBA2.Exceptions;
using ZAL_PROBA2.Repositories;

namespace ZAL_PROBA2.Services;

public interface ITasksService
{
    public Task<List<GetTasksDto>> GetTasksForProject(int idProject);

    public Task CreateTask(CreateTaskDto dto);
}

public class TasksService(ITaskRepository taskRepository, 
    IUserRepository userRepository, IProjectRepository projectRepository, IAccessRepository accessRepository) : ITasksService
{
    public async Task<List<GetTasksDto>> GetTasksForProject(int idProject)
    {
        return await taskRepository.GetForProject(idProject);
    }


    public async Task CreateTask(CreateTaskDto dto)
    {
        var reporter = await userRepository.GetOneById(dto.IdReporter);
        if (reporter == null)
        {
            throw new UserNotFoundException();
        }
        if (await accessRepository.GetByUserIdAndProject(dto.IdReporter, dto.IdProject) == null)
        {
            throw new UserHasNoPermissionsException();
        }

        if (dto.IdAssignee != null)
        { 
            var assignee = await userRepository.GetOneById((int)dto.IdAssignee);
            if (assignee == null)
            {
                throw new AssigneeUserNotFoundException();
            }
            
            if (await accessRepository.GetByUserIdAndProject((int)dto.IdAssignee, dto.IdProject) == null)
            {
                throw new UserHasNoPermissionsException();
            }
        }

        var project = await projectRepository.GetOneById(dto.IdProject);
        if (project == null)
        {
            throw new ProjectNotFoundException();
        }
        
        var task = new Models.Task()
        {
            IdReporter = dto.IdReporter,
            IdProject = dto.IdProject,
            IdAssignee = dto.IdAssignee ?? project.IdDefaultAssignee,
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.Now
        };

        await taskRepository.Add(task);
    }
}