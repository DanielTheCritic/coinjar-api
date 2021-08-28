namespace CoinJar.Core.Coins
{
  public interface ICoin
  {
    decimal Amount { get; set; }

    decimal Volume { get; set; }
  }
}

