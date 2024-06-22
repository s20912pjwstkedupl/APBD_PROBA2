namespace ZAL_PROBA2.Models;

public class Access
{
    public int IdUser { get; set; }
    public int IdProject { get; set; }
    
    public virtual User VUser { get; set; }
    public virtual Project VProject { get; set; }

}