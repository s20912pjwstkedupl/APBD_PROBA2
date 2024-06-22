using Microsoft.AspNetCore.Mvc;
using ZAL_PROBA2.Dtos;
using ZAL_PROBA2.Exceptions;
using ZAL_PROBA2.Services;

namespace ZAL_PROBA2.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TaskController(ITasksService tasksService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetForProject([FromQuery] int projectId)
    {
        try
        {
            var tasks = await tasksService.GetTasksForProject(projectId);
            return Ok(new TaskResponseDto()
            {
                Tasks = tasks
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateTaskDto dto)
    {
        try
        {
            await tasksService.CreateTask(dto);
            return Created();
        }
        catch (AssigneeUserNotFoundException)
        {
            return BadRequest("Assignee user not found");
        }
        catch (ProjectNotFoundException)
        {
            return BadRequest("Project not found");
        }
        catch (UserHasNoPermissionsException)
        {
            return BadRequest("User has no permissions to project");
        }
        catch (UserNotFoundException)
        {
            return BadRequest("User not found");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}