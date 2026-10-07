using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeYonetimiAPI.Controllers;
using ProjeYonetimiAPI.Models;
using Xunit;

namespace ProjeYonetimiAPI.Tests;

public class TasksControllerTests
{
    // Her test için sıfırdan, temiz bir hafıza içi (In-Memory) veritabanı oluşturur
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public void UpdateTaskStatus_InvalidAssignee_ReturnsNotFound()
    {
        // 1. ARRANGE (Hazırlık): Veritabanını ve controller'ı hazırla
        var context = GetInMemoryDbContext();
        
        // Veritabanına mevcut bir görev ekliyoruz ancak sisteme hiç kullanıcı eklemiyoruz
        var task = new ProjectTask { Id = 1, Title = "Test Görev", ProjectId = 1 };
        context.ProjectTasks.Add(task);
        context.SaveChanges();

        var controller = new TasksController(context);

        // 2. ACT (Eylem): Dokümanda istenen 'geçersiz kullanıcıya görev atama' işlemini yapıyoruz
        // Görevi sistemde olmayan 99 ID'li kullanıcıya atamaya çalışıyoruz
        var result = controller.UpdateTaskStatus(1, TaskState.InProgress, 99);

        // 3. ASSERT (Doğrulama): Sistemin bu geçersiz işleme doğru tepkiyi (404 Not Found) verip vermediğini test et
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        
        // Dönen mesajın bizim beklediğimiz hata mesajı olduğunu doğrula
        Assert.Contains("Atanacak kullanıcı bulunamadı", notFoundResult.Value?.ToString());
    }
}