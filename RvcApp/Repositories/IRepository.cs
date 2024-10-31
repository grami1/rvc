using RvcApp.Models;

namespace RvcApp.Repositories;

public interface IRepository
{
    void Save(Execution execution);
}