using RvcApp.Exceptions;
using RvcApp.Models;
using RvcApp.Repositories;

namespace RvcApp.Services;

public class RvcService : IRvcService
{
    private readonly ICleaner _cleaner;
    private readonly ILogger<RvcService> _logger;
    private readonly IRepository _repository;

    public RvcService(ICleaner cleaner, ILogger<RvcService> logger, IRepository repository)
    {
        _cleaner = cleaner;
        _logger = logger;
        _repository = repository;
    }
    
    public Execution RunRvc(RvcPath rvcPath)
    {
        var commands = rvcPath.Commands;
        var startCoordinate = rvcPath.Start;

        try
        {
            var startTime = DateTime.UtcNow;

            var cleanedZones = _cleaner.Clean(startCoordinate, commands);

            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalSeconds;

            var execution = new Execution
            {
                Timestamp = DateTime.UtcNow.ToString("O"), 
                Commands = commands.Count, 
                Result = cleanedZones, 
                Duration = duration
            };
            _repository.Save(execution);

            return execution;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to run RVC. Error: {}", e.Message);
            throw new RvcException("Failed to run RVC");
        }
    }
}