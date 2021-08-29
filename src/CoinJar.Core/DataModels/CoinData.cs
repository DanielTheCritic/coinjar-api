using CoinJar.Core.Coins;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinJar.Core.DataModels
{
  public class CoinData : ICoin
  {
    public decimal Amount { get; set; }

    public decimal Volume { get; set; }
  }
}
