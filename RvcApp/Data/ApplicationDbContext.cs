using Microsoft.EntityFrameworkCore;
using RvcApp.Models;

namespace RvcApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public ApplicationDbContext() { }

    public DbSet<Execution> Executions { get; init; }
}