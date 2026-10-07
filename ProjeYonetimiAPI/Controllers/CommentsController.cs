using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeYonetimiAPI.Models;
using System.Security.Claims;

namespace ProjeYonetimiAPI.Controllers;

public class CreateCommentRequest
{
    public string Content { get; set; } = string.Empty;
}

[Route("api/tasks/{taskId}/comments")] // REST standartlarına uygun iç içe yönlendirme
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Görevin Yorumlarını Listele
    [HttpGet]
    public IActionResult GetComments(int taskId)
    {
        var taskExists = _context.ProjectTasks.Any(t => t.Id == taskId);
        if (!taskExists) return NotFound(new { message = "Görev bulunamadı." });

        var comments = _context.Comments
            .Where(c => c.TaskId == taskId)
            .Include(c => c.User)
            .Select(c => new
            {
                c.Id,
                c.Content,
                c.CreatedAt,
                CreatedBy = c.User != null ? c.User.FirstName + " " + c.User.LastName : "Bilinmiyor"
            })
            .OrderByDescending(c => c.CreatedAt) // En yeni yorumlar üstte görünsün
            .ToList();

        return Ok(comments);
    }

    // 2. Göreve Yorum Ekle
    [HttpPost]
    public IActionResult CreateComment(int taskId, [FromBody] CreateCommentRequest request)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized("Geçersiz kullanıcı kimliği.");
        }

        var taskExists = _context.ProjectTasks.Any(t => t.Id == taskId);
        if (!taskExists) return NotFound(new { message = "Yorum eklenecek görev bulunamadı." });

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest(new { message = "Yorum içeriği boş olamaz." });
        }

        var newComment = new Comment
        {
            TaskId = taskId,
            UserId = userId,
            Content = request.Content,
            CreatedAt = DateTime.Now
        };

        _context.Comments.Add(newComment);
        _context.SaveChanges();

        return Ok(new { message = "Yorum başarıyla eklendi.", commentId = newComment.Id });
    }
}