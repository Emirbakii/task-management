using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeYonetimiAPI.Models;
using System.Security.Claims;

namespace ProjeYonetimiAPI.Controllers;

// DTO: Proje oluştururken alacağımız veriler
public class CreateProjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
}

// DTO: Proje güncellerken alacağımız veriler
public class UpdateProjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}

[Route("api/[controller]")]
[ApiController]
[Authorize] // Sadece Token'ı olanlar erişebilir
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("test")]
    public IActionResult TestToken()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        return Ok($"Tebrikler! Sisteme yetkili olarak girdiniz. Kimliğinizdeki e-posta: {userEmail}");
    }

    // 1. Projeleri Listele (GET: api/projects)
    [HttpGet]
    public IActionResult GetProjects()
    {
        var projects = _context.Projects
            .Include(p => p.CreatedByUser)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.StartDate,
                p.EndDate,
                Status = p.Status.ToString(),
                p.CreatedAt,
                CreatedBy = p.CreatedByUser != null ? p.CreatedByUser.FirstName + " " + p.CreatedByUser.LastName : "Bilinmiyor"
            })
            .ToList();

        return Ok(projects);
    }

    // 2. Yeni Proje Oluştur (POST: api/projects)
    [HttpPost]
    public IActionResult CreateProject([FromBody] CreateProjectRequest request)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized("Geçersiz kullanıcı kimliği.");
        }

        var newProject = new Project
        {
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            CreatedByUserId = userId,
            CreatedAt = DateTime.Now
        };

        _context.Projects.Add(newProject);
        _context.SaveChanges();

        return Ok(new { message = "Proje başarıyla oluşturuldu.", projectId = newProject.Id });
    }

    // 3. Projeyi Güncelle (PUT: api/projects/{id})
    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, [FromBody] UpdateProjectRequest request)
    {
        var project = _context.Projects.Find(id);
        if (project == null)
        {
            return NotFound(new { message = "Güncellenecek proje bulunamadı." });
        }

        // Proje bilgilerini güncelliyoruz
        project.Name = request.Name;
        project.Description = request.Description;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.Status = request.Status;

        _context.SaveChanges();

        return Ok(new { message = "Proje başarıyla güncellendi." });
    }

    // 4. Projeyi Sil (DELETE: api/projects/{id})
    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var project = _context.Projects.Find(id);
        if (project == null)
        {
            return NotFound(new { message = "Silinecek proje bulunamadı." });
        }

        _context.Projects.Remove(project);
        _context.SaveChanges();

        return Ok(new { message = "Proje başarıyla silindi." });
    }
}