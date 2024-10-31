using RvcApp.Models;

namespace RvcApp.Services;

public interface IRvcService
{
    Execution RunRvc(RvcPath rvcPath);
}