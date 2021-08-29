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

    public CoinJarController(ICoinJarManager coinJarManager)
    {
      _manager = coinJarManager;
    }

    /// <summary>
    /// Gets the sum of all the coins in the jar in the equivalent currency.
    /// </summary>
    /// <returns>An object with a TotalAmount field.</returns>
    [HttpGet]
    [Produces("application/json")]
    public CoinJarAmount GetAmount()
    {
      return _manager.GetCoinJarAmount();
    }

    /// <summary>
    /// Adds a coin to the coin jar.
    /// </summary>
    /// <param name="coin">Accepts a USDCoin, an implementation of ICoin.</param>
    /// <returns></returns>
    /// <response code="200">An empty response if adding the coin was successful.</response>
    /// <response code="400">If the coin passed into the request was invalid.</response>  
    [HttpPost]
    [Route("coins")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ErrorResponse))]
    public IActionResult AddCoin([FromBody]USDCoin coin)
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
    [ProducesResponseType(200)]
    public IActionResult Reset()
    {
      _manager.Reset();
      return Ok();
    }
  }
}
