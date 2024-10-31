using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RvcApp.Controllers;
using RvcApp.Exceptions;
using RvcApp.Models;
using RvcApp.Services;
using Xunit;

namespace RvcApp.Tests.Controllers;

public class RvcControllerTest
{

    private readonly Mock<IRvcService> _rvcServiceMock;
    private readonly RvcController _controller;

    public RvcControllerTest()
    {
        _rvcServiceMock = new Mock<IRvcService>();
        _controller = new RvcController(_rvcServiceMock.Object);
    }
    
    [Fact]
    public void RunRvc_WithSuccessRunning_ShouldReturnExecution()
    {
        var commands = new List<Command> { new Command(Direction.East, 2)}; 
        var rvcPath = new RvcPath(new Coordinate(0, 0), commands);

        var execution = new Execution
        {
            Timestamp = DateTime.UtcNow.ToString("O"),
            Commands = 1,
            Result = 5,
            Duration = 0.01
        };

        _rvcServiceMock.Setup(s => s.RunRvc(It.IsAny<RvcPath>()))
            .Returns(execution);
        
        var result = _controller.RunRvc(rvcPath) as OkObjectResult;
        
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(execution, result.Value);
    }
    
    [Fact]
    public void RunRvc_WithFailRunning_ShouldThrowException()
    {
        var commands = new List<Command> { new Command(Direction.East, 2)}; 
        var rvcPath = new RvcPath(new Coordinate(0, 0), commands);
        
        _rvcServiceMock.Setup(s => s.RunRvc(It.IsAny<RvcPath>()))
            .Throws(new RvcException("Failed to run RVC"));
        
        var exception = Assert.Throws<RvcException>(() => _controller.RunRvc(rvcPath));
        
        Assert.Equal("Failed to run RVC", exception.Message);
    }
}