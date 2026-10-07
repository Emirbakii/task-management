namespace ProjeYonetimiAPI.Models;

public enum TaskState
{
    ToDo,
    InProgress,
    Completed,
    Cancelled
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public class ProjectTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Görevin ait olduğu proje
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    // Görevi oluşturan kullanıcı
    public int CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    // Görevin atandığı kullanıcı (Henüz kimseye atanmamış olabilir, o yüzden int? yaptık)
    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }

    // Durum ve Öncelik
    public TaskState Status { get; set; } = TaskState.ToDo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    // Zaman Bilgileri
    public DateTime? DueDate { get; set; } // Teslim tarihi (Boş bırakılabilir)
    public int EstimatedHours { get; set; } // Tahmini efor (Saat)
    public int LoggedHours { get; set; } // Harcanan zaman (Saat)
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Göreve yapılan yorumların listesi
    public List<Comment> Comments { get; set; } = new();
}