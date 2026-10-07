using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeYonetimiAPI.Services; // Servisleri dahil ettik

namespace ProjeYonetimiAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    // Dependency Injection: Sistemi _dashboardService'i otomatik olarak buraya enjekte eder
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public IActionResult GetDashboardStats()
    {
        // İş mantığı serviste yapılıyor, Controller sadece sonucu dönüyor
        var stats = _dashboardService.GetDashboardStatistics();
        return Ok(stats);
    }
}