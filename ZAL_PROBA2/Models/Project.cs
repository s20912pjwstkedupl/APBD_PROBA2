namespace ZAL_PROBA2.Models;

public class Project
{
    public int IdProject { get; set; }
    public string Name { get; set; }
    public int IdDefaultAssignee { get; set; }
    
    public virtual ICollection<Access> VAccesses { get; set; }
    public virtual ICollection<Task> VTasks { get; set; }
    
    public virtual User VDefaultAssignee { get; set; }

}