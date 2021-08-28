using CoinJar.Core.Coins;
using CoinJarApi.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoinJarApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CoinJarController : ControllerBase
  {
    private readonly ICoinJarManager _manager;
    private readonly ILogger<CoinJarController> _logger;

    public CoinJarController(ICoinJarManager coinJarManager, ILogger<CoinJarController> logger)
    {
      _logger = logger;
      _manager = coinJarManager;
    }

    [HttpGet]
    public CoinJarAmount GetAmount()
    {
      return _manager.GetCoinJarAmount();
    }

    [HttpPost]
    public IActionResult AddCoin(USDCoin coin)
    {
      _manager.AddCoin(coin);
      return Ok();
    }

    [HttpDelete]
    public IActionResult Reset()
    {
      _manager.Reset();
      return Ok();
    }
  }
}
