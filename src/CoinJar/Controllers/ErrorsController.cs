using CoinJar.Core.Coins;
using CoinJar.Core.Constants;
using CoinJar.Core.Exceptions;
using CoinJarApi.Managers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoinJarApi.Controllers
{
  [ApiController]
  [ApiExplorerSettings(IgnoreApi = true)]
  public class ErrorsController : ControllerBase
  {
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
      }

      return response;
    }
  }
}
