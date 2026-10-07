namespace ProjeYonetimiAPI.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Hangi göreve yapıldığını belirten özellikler (Bu eksikti)
    public int TaskId { get; set; }
    public ProjectTask? Task { get; set; }

    // Yorumu yapan kullanıcı
    public int UserId { get; set; }
    public User? User { get; set; }
}