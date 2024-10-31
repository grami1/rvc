using RvcApp.Data;
using RvcApp.Models;

namespace RvcApp.Repositories;

public class ExecutionRepository : IRepository
{
    private readonly ApplicationDbContext _context;

    public ExecutionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Save(Execution execution)
    {
        _context.Executions.Add(execution);
        _context.SaveChanges();
    }
}