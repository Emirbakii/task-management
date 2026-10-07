using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeYonetimiAPI.Models;
using System.Security.Claims;

namespace ProjeYonetimiAPI.Controllers;

// DTO: Görev oluştururken dışarıdan alacağımız veriler
public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public int EstimatedHours { get; set; }
}

[Route("api/[controller]")]
[ApiController]
[Authorize] // Sadece giriş yapmış kullanıcılar görevleri görebilir ve yönetebilir
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Görevleri Listele ve Filtrele (FR-08 Arama ve Filtreleme)
    // Örnek kullanım: GET api/tasks?projectId=1&status=0
    [HttpGet]
    public IActionResult GetTasks([FromQuery] int? projectId, [FromQuery] TaskState? status)
    {
        var query = _context.ProjectTasks
            .Include(t => t.Project)
            .Include(t => t.AssignedUser)
            .AsQueryable();

        // FR-08: Query parametreleriyle filtreleme desteği
        if (projectId.HasValue)
        {
            query = query.Where(t => t.ProjectId == projectId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        var tasks = query.Select(t => new
        {
            t.Id,
            t.Title,
            t.Description,
            Status = t.Status.ToString(),
            Priority = t.Priority.ToString(),
            ProjectName = t.Project != null ? t.Project.Name : "Bilinmiyor",
            AssignedTo = t.AssignedUser != null ? t.AssignedUser.FirstName + " " + t.AssignedUser.LastName : "Atanmadı",
            t.DueDate
        }).ToList();

        return Ok(tasks);
    }

    // 2. Görev Oluştur (FR-04)
    [HttpPost]
    public IActionResult CreateTask([FromBody] CreateTaskRequest request)
    {
        // Token'dan giriş yapan kullanıcıyı bul
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized("Geçersiz kullanıcı kimliği.");
        }

        // Projenin var olup olmadığını kontrol et (404 Not Found Kuralı)
        var projectExists = _context.Projects.Any(p => p.Id == request.ProjectId);
        if (!projectExists)
        {
            return NotFound(new { message = "Belirtilen proje bulunamadı." });
        }

        var newTask = new ProjectTask
        {
            Title = request.Title,
            Description = request.Description,
            ProjectId = request.ProjectId,
            CreatedByUserId = userId,
            Status = TaskState.ToDo, // FR-05: Varsayılan durum
            Priority = request.Priority,
            DueDate = request.DueDate,
            EstimatedHours = request.EstimatedHours,
            CreatedAt = DateTime.Now
        };

        _context.ProjectTasks.Add(newTask);
        _context.SaveChanges();

        return Ok(new { message = "Görev başarıyla oluşturuldu.", taskId = newTask.Id });
    }

    // 3. Görevi Birine Ata veya Durumunu Güncelle (PUT)
    [HttpPut("{id}/update-status")]
    public IActionResult UpdateTaskStatus(int id, [FromQuery] TaskState newState, [FromQuery] int? assignToUserId)
    {
        var task = _context.ProjectTasks.Find(id);
        if (task == null)
        {
            return NotFound(new { message = "Görev bulunamadı." });
        }

        task.Status = newState;

        if (assignToUserId.HasValue)
        {
            var userExists = _context.Users.Any(u => u.Id == assignToUserId.Value);
            if (!userExists) return NotFound(new { message = "Atanacak kullanıcı bulunamadı." });
            
            task.AssignedUserId = assignToUserId.Value;
        }

        _context.SaveChanges();
        return Ok(new { message = "Görev güncellendi." });
    }
}