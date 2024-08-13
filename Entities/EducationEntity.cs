using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalWebsiteAPI.Entities;

[Table("education")]
public class EducationEntity
{
    [Key]
    public int id { get; set; }
    public int user_id { get; set; }
    [ForeignKey("user_id")]
    public UserEntity? user { get; set; }
    [StringLength(200)]
    public string? nama_sekolah { get; set; }
    [StringLength(200)]
    public string? jurusan { get; set; }

}