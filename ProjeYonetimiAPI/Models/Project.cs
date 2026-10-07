namespace ProjeYonetimiAPI.Models;

// Proje durumlarını standartlaştırmak için Enum kullanıyoruz
public enum ProjectStatus
{
    Planning,
    Active,
    Completed,
    Cancelled
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    
    // Proje henüz bitmemiş olabileceği için bitiş tarihi boş (null) bırakılabilir. (DateTime yanındaki '?' bunu sağlar)
    public DateTime? EndDate { get; set; } 
    
    // Varsayılan olarak yeni oluşturulan bir proje "Planning" (Planlama) aşamasında başlar
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    
    // Projeyi oluşturan kullanıcının ID'si (Foreign Key)
    public int CreatedByUserId { get; set; }
    
    // EF Core'un tabloları birbirine bağlaması için Navigasyon Özelliği
    public User? CreatedByUser { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // Bir projenin içinde birden fazla görev olabilir
    public List<ProjectTask> Tasks { get; set; } = new();
}