using CoinJar.Core.Coins;

namespace CoinJarApi.Managers
{
  public interface ICoinJarManager
  {
    public void AddCoin(ICoin coin);

    public void Reset();

    public CoinJarAmount GetCoinJarAmount();
  }
}
