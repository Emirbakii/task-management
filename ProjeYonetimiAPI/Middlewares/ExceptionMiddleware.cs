using System.Net;
using System.Text.Json;

namespace ProjeYonetimiAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Sorun yoksa isteği bir sonraki adıma geçir
            await _next(context);
        }
        catch (Exception ex)
        {
            // Hata çıkarsa yakala, Logla ve kullanıcıya güvenli bir formatta dön
            _logger.LogError(ex, "Sistemde beklenmeyen bir hata oluştu. Path: {Path}", context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        // 500 Internal Server Error (FR: Hata Yönetimi Beklentisi)
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Kullanıcıya sistem detaylarını (Stack Trace vb.) sızdırmayan güvenli mesaj
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Sunucu tarafında beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
            DetailedMessage = exception.Message // Sadece geliştirme ortamında gösterilebilir, şimdilik basit tutuyoruz
        };

        var json = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(json);
    }
}