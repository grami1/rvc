using System.Collections.Generic;
using RvcApp.Models;
using RvcApp.Services;
using Xunit;

namespace RvcApp.Tests.Services;

public class RvcCleanerTest
{

    [Fact]
    public void Clean_WithNoCommands_ShouldReturnOneCleanedZone()
    {
        var startCoordinate = new Coordinate(10, 22);
        var commands = new List<Command>();
        var rvcCleaner = new RvcCleaner();

        var cleanedZones = rvcCleaner.Clean(startCoordinate, commands);
        
        Assert.Equal(1, cleanedZones);
    }

    [Fact]
    public void Clean_WithMultipleCommands_ShouldReturnCorrectNumberOfZones()
    {
        var startCoordinate = new Coordinate(10, 22);
        var commands = new List<Command>
        {
            new Command(Direction.East, 2), 
            new Command(Direction.North, 1)
        };
        var rvcCleaner = new RvcCleaner();

        var cleanedZones = rvcCleaner.Clean(startCoordinate, commands);
        
        Assert.Equal(4, cleanedZones);
    }
    
    [Fact]
    public void Clean_WithBacktrackingCommands_ShouldNotDoubleCountZones()
    {
        var startCoordinate = new Coordinate(10, 22);
        var commands = new List<Command>
        {
            new Command(Direction.East, 2), 
            new Command(Direction.West, 2)
        };
        var rvcCleaner = new RvcCleaner();

        var cleanedZones = rvcCleaner.Clean(startCoordinate, commands);
        
        Assert.Equal(3, cleanedZones);
    }
    
    [Fact]
    public void Clean_WithComplexPath_ShouldCountCorrectZones()
    {
        var startCoordinate = new Coordinate(1, 1);
        var commands = new List<Command>
        {
            new Command(Direction.North, 2), 
            new Command(Direction.East, 2), 
            new Command(Direction.South, 2), 
            new Command(Direction.West, 2), 
            new Command(Direction.South, 1)
        };
        var rvcCleaner = new RvcCleaner();
    
        var cleanedZones = rvcCleaner.Clean(startCoordinate, commands);
   
        Assert.Equal(9, cleanedZones);
    }
}