using CoinJar.Core.Constants;
using CoinJar.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace CoinJarApi.Controllers
{
  [ApiController]
  [ApiExplorerSettings(IgnoreApi = true)]
  public class ErrorsController : ControllerBase
  {
    private readonly ILogger _logger;

    public ErrorsController(ILogger logger)
    {
      _logger = logger;
    }
    [Route("error")]
    public ErrorResponse Error()
    {
      var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

      var response = new ErrorResponse
      {
        Code = CoinJarErrorCodes.General,
        Message = context.Error.Message
      };

      if(context.Error is CoinJarException coinJarException)
      {
        Response.StatusCode = 400;
        response.Code = coinJarException.ErrorCode;
        _logger.Debug(coinJarException);
      }
      else
      {
        Response.StatusCode = 500;
        _logger.Error(context.Error);
      }



      return response;
    }
  }
}
