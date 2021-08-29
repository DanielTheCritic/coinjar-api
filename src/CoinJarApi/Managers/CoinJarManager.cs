using CoinJar.Core.CoinJars;
using CoinJar.Core.Coins;

namespace CoinJarApi.Managers
{
  public class CoinJarManager : ICoinJarManager
  {
    private readonly ICoinJar _coinJar;

    public CoinJarManager(ICoinJar coinJar)
    {
      _coinJar = coinJar;
    }

    public void AddCoin(ICoin coin)
    {
      _coinJar.AddCoin(coin);
    }

    public CoinJarAmount GetCoinJarAmount()
    {
      return new CoinJarAmount
      {
        TotalAmount = _coinJar.GetTotalAmount()
      };
    }

    public void Reset()
    {
      _coinJar.Reset();
    }
  }
}
