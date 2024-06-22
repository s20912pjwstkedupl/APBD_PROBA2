namespace ZAL_PROBA2.Models;

public class User
{
    public int IdUser { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public virtual ICollection<Access> VAccesses { get; set; }
    public virtual ICollection<Project> VProjects { get; set; }
    public virtual ICollection<Task> VTaskAssignees { get; set; }
    public virtual ICollection<Task> VTaskReporters { get; set; }
}