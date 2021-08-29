using CoinJar.Core.Coins;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinJar.Tests
{
  public class MockCoin : ICoin
  {
    public MockCoin(decimal amount, decimal volume)
    {
      Amount = amount;
      Volume = volume;
    }

    public decimal Amount { get; set; }
    public decimal Volume { get; set; }
  }
}
