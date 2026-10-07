using ProjeYonetimiAPI.Models;

namespace ProjeYonetimiAPI.Services;

// 1. Interface (Arayüz): Servisin hangi işleri yapacağını tanımlar (Sözleşme)
public interface IDashboardService
{
    object GetDashboardStatistics();
}

// 2. Class (Sınıf): İşin asıl yapıldığı yer (Business Logic)
public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public object GetDashboardStatistics()
    {
        // Controller'daki tüm hesaplama mantığını buraya taşıdık
        var totalProjects = _context.Projects.Count();
        var activeProjects = _context.Projects.Count(p => p.Status == ProjectStatus.Active);
        
        var totalTasks = _context.ProjectTasks.Count();
        var completedTasks = _context.ProjectTasks.Count(t => t.Status == TaskState.Completed);
        var inProgressTasks = _context.ProjectTasks.Count(t => t.Status == TaskState.InProgress);
        
        var overdueTasks = _context.ProjectTasks.Count(t => 
            t.DueDate.HasValue && 
            t.DueDate.Value < DateTime.Now && 
            t.Status != TaskState.Completed);

        return new
        {
            Projects = new { Total = totalProjects, Active = activeProjects },
            Tasks = new { Total = totalTasks, Completed = completedTasks, InProgress = inProgressTasks, Overdue = overdueTasks }
        };
    }
}