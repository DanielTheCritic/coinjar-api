using CoinJar.Core.Coins;
using CoinJarApi.Managers;
using Microsoft.AspNetCore.Mvc;

namespace CoinJarApi.Controllers
{
  /// <summary>
  /// The CoinJar controller that handles all CRUD operations relating to a single instance of a coin jar.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class CoinJarController : ControllerBase
  {
    private readonly ICoinJarManager _manager;

    /// <summary>
    /// A constructor that accepts an implementation of ICoinJarManager.
    /// </summary>
    /// <param name="coinJarManager">The ICoinJarManager.</param>
    public CoinJarController(ICoinJarManager coinJarManager)
    {
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
