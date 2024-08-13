using Microsoft.EntityFrameworkCore;
using PersonalWebsiteAPI.Entities;

namespace PersonalWebsiteAPI.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<UserEntity> User { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
}
