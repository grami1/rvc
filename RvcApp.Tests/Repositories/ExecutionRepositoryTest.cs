using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RvcApp.Data;
using RvcApp.Models;
using RvcApp.Repositories;
using Xunit;

namespace RvcApp.Tests.Repositories;

public class ExecutionRepositoryTest
{
    private readonly ApplicationDbContext _context;
    private readonly ExecutionRepository _repository;

    public ExecutionRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ExecutionRepository(_context);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public void SaveExecution_ShouldSaveExecutionDetailsToDatabase()
    {
        var execution = new Execution
        {
            Timestamp = DateTime.UtcNow.ToString("O"),
            Commands = 1,
            Result = 1,
            Duration = 0.0001
        };

        _repository.Save(execution);

        var savedExecution = _context.Executions.FirstOrDefault(e => e.Commands == 1 && e.Result == 1);

        Assert.NotNull(savedExecution);
        Assert.Equal(1, savedExecution.Result);
        Assert.Equal(1, savedExecution.Commands);
    }
}