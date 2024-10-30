using RvcApp.Models;

namespace RvcApp.Services;

public interface ICleaner
{
    int Clean(Coordinate startCoordinate, List<Command> commands);
}