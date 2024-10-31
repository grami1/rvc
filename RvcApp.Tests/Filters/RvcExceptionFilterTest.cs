using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RvcApp.Exceptions;
using RvcApp.Filters;
using Xunit;

namespace RvcApp.Tests.Filters;

public class RvcExceptionFilterTest
{

    [Fact]
    public void RvcExceptionFilter_ReturnsInternalServerError_OnRvcException()
    {
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
        };
        
        var exceptionContext = new ExceptionContext(
            actionContext,
            new List<IFilterMetadata>()
        )
        {
            Exception = new RvcException("Failed to run RVC")
        };

        var filter = new RvcExceptionFilter();

        filter.OnException(exceptionContext);
        
        Assert.True(exceptionContext.ExceptionHandled);
        var result = exceptionContext.Result as ObjectResult;
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
        Assert.Equal("{ message = Failed to run RVC }", result.Value.ToString());
    }
}