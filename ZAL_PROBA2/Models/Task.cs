namespace ZAL_PROBA2.Models;

public class Task
{
    public int IdTask { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IdProject { get; set; }
    public int IdReporter { get; set; }
    public int IdAssignee { get; set; }
    
    public virtual User VAssignee { get; set; }
    public virtual User VReporter { get; set; }
    public virtual Project VProject { get; set; }

}