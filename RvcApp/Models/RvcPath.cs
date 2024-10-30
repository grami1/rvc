namespace RvcApp.Models;

public record RvcPath(Coordinate StartCoordinate, List<Command> Commands);