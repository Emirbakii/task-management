namespace ProjeYonetimiAPI.Models;

public class User
{
    public int Id { get; set; } // id
    
    public string FirstName { get; set; } = string.Empty; // ad
    
    public string LastName { get; set; } = string.Empty; // soyad
    
    public string Email { get; set; } = string.Empty; // e-posta
    
    // Şifreler veritabanına asla düz metin (123456 vb.) olarak kaydedilmez.
    // Şifrelenmiş (hash'lenmiş) halini tutacağımız için adını PasswordHash yaptık.
    public string PasswordHash { get; set; } = string.Empty; 
    
    // Admin, Manager veya standart User gibi rolleri tutacak alan.
    // Varsayılan olarak sisteme kayıt olanlara "User" rolünü veriyoruz.
    public string Role { get; set; } = "Employee"; 
    
    public DateTime CreatedAt { get; set; } = DateTime.Now; // oluşturulma tarihi
    
    // Aktif/Pasif durumu için mantıksal (boolean) veri tipi (true/false) kullanılır.
    // Yeni oluşturulan kullanıcı varsayılan olarak aktiftir (true).
    public bool IsActive { get; set; } = true; 
    public List<Project> CreatedProjects { get; set; } = new();
    // Kullanıcının atandığı görevler, oluşturduğu görevler ve yaptığı yorumlar
    public List<ProjectTask> AssignedTasks { get; set; } = new();
    public List<ProjectTask> CreatedTasks { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}