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

    /// <summary>
    /// Gets the sum of all the coins in the jar in the equivalent currency.
    /// </summary>
    /// <returns>An object with a TotalAmount field.</returns>
    [HttpGet]
    public CoinJarAmount GetAmount()
    {
      return _manager.GetCoinJarAmount();
    }

    /// <summary>
    /// Adds a coin to the coin jar.
    /// </summary>
    /// <param name="coin">Accepts a USDCoin, an implementation of ICoin.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("coins")]
    public IActionResult AddCoin(USDCoin coin)
    {
      _manager.AddCoin(coin);
      return Ok();
    }

    /// <summary>
    /// Removes all coins from the coin jar.
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("coins")]
    public IActionResult Reset()
    {
      _manager.Reset();
      return Ok();
    }
  }
}
