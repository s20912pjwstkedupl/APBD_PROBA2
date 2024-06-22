namespace ZAL_PROBA2.Dtos;

public class GetTasksDto
{
    public int IdTask { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IdProject { get; set; }
    public int IdReporter { get; set; }
    public GetUserTaskDto Reporter { get; set; }
    public int IdAssignee { get; set; }
    public GetUserTaskDto Assignee { get; set; }
}

public class GetUserTaskDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class TaskResponseDto
{
    public List<GetTasksDto> Tasks { get; set; }
}

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int IdProject { get; set; }
    public int IdReporter { get; set; }
    public int? IdAssignee { get; set; }
}