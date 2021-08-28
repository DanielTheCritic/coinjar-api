using CoinJar.Core.Coins;

namespace CoinJar.Core.CoinJars
{
  public interface ICoinJar
  {
    void AddCoin(ICoin coin);

    decimal GetTotalAmount();

    void Reset();
  }
}