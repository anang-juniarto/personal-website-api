using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalWebsiteAPI.Entities;

[Table("User")]
public class UserEntity
{
    [Key]
    public int id { get; set; }
    [StringLength(200)]
    public string name { get; set; } = string.Empty;
    [StringLength(200)]
    public string email { get; set; } = string.Empty;
    [Column(TypeName = "text")]
    public string image { get; set; } = string.Empty;
    [StringLength(200)]
    public string job_title { get; set; } = string.Empty;
    [StringLength(200)]
    public string whatsapp { get; set; } = string.Empty;
    [StringLength(200)]
    public string github_url { get; set; } = string.Empty;
    [StringLength(200)]
    public string linkedin_url { get; set; } = string.Empty;
    [Column(TypeName = "text")]
    public string bio { get; set; } = string.Empty;
}
