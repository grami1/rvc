using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using RvcApp.Exceptions;
using RvcApp.Models;
using RvcApp.Repositories;
using RvcApp.Services;
using Xunit;

namespace RvcApp.Tests.Services;

public class RvcServiceTest
{

    private readonly Mock<ICleaner> _cleanerMock;
    private readonly Mock<ILogger<RvcService>> _loggerMock;
    private readonly Mock<IRepository> _repositoryMock;
    private readonly RvcService _service;

    public RvcServiceTest()
    {
        _cleanerMock = new Mock<ICleaner>();
        _loggerMock = new Mock<ILogger<RvcService>>();
        _repositoryMock = new Mock<IRepository>();
        _service = new RvcService(_cleanerMock.Object, _loggerMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void RunRvc_WithSuccessRunning_ShouldReturnExecution()
    {
        var commands = new List<Command> { new Command(Direction.East, 2)}; 
        var rvcPath = new RvcPath(new Coordinate(0, 0), commands);

        var cleanedZones = 2;
        _cleanerMock.Setup(c => c.Clean(It.IsAny<Coordinate>(), It.IsAny<List<Command>>()))
            .Returns(cleanedZones);

        var execution = _service.RunRvc(rvcPath);

        Assert.NotNull(execution);
        Assert.Equal(rvcPath.Commands.Count, execution.Commands);
        Assert.Equal(cleanedZones, execution.Result);
        Assert.True(execution.Duration > 0);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Execution>()), Times.Once);
    }
    
    [Fact]
    public void RunRvc_WithFailedRunning_ShouldThrowException()
    {
        var commands = new List<Command> { new Command(Direction.East, 2)}; 
        var rvcPath = new RvcPath(new Coordinate(0, 0), commands);
        
        _cleanerMock.Setup(c => c.Clean(It.IsAny<Coordinate>(), It.IsAny<List<Command>>()))
            .Throws(new Exception("Something went wrong"));
        
        var exception = Assert.Throws<RvcException>(() => _service.RunRvc(rvcPath));
        
        Assert.Equal("Failed to run RVC", exception.Message);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Execution>()), Times.Never);
    }
    
    [Fact]
    public void RunRvc_WithFailedSavingExecution_ShouldThrowException()
    {
        var commands = new List<Command> { new Command(Direction.East, 2)}; 
        var rvcPath = new RvcPath(new Coordinate(0, 0), commands);
        
        var cleanedZones = 2;
        _cleanerMock.Setup(c => c.Clean(It.IsAny<Coordinate>(), It.IsAny<List<Command>>()))
            .Returns(cleanedZones);
        _repositoryMock.Setup(r => r.Save(It.IsAny<Execution>()))
            .Throws(new Exception("Something went wrong"));
        
        var exception = Assert.Throws<RvcException>(() => _service.RunRvc(rvcPath));
        
        Assert.Equal("Failed to run RVC", exception.Message);
    }
}