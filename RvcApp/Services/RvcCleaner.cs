using RvcApp.Models;

namespace RvcApp.Services;

public class RvcCleaner : ICleaner
{
    public int Clean(Coordinate startCoordinate, List<Command> commands)
    {
        var cleanedZones = new HashSet<Coordinate>();

        var x = startCoordinate.X;
        var y = startCoordinate.Y;

        // Starting position is also considered cleaned
        cleanedZones.Add(new Coordinate(x, y));

        foreach (var (direction, steps) in commands)
        {
            for (var i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case Direction.North:
                        y++;
                        break;
                    case Direction.South:
                        y--;
                        break;
                    case Direction.East:
                        x++;
                        break;
                    case Direction.West:
                        x--;
                        break;
                    default:
                        throw new ArgumentException("Incorrect direction: " + direction);
                }
                cleanedZones.Add(new Coordinate(x, y));
            }
        }
        return cleanedZones.Count;
    }
}