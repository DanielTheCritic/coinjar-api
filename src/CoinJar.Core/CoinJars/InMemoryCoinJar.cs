using CoinJar.Core.Coins;
using CoinJar.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinJar.Core.CoinJars
{
  public class InMemoryCoinJar : ICoinJar
  {
    private readonly decimal _volume;
    private readonly List<ICoin> _coins;

    public InMemoryCoinJar()
    {
      _coins = new List<ICoin>();
      _volume = CoinJarVolumes.DEFAULT;
    }

    public void AddCoin(ICoin coin)
    {
      var filledVolume = GetFilledVolume();
      if(filledVolume + coin.Volume > _volume)
      {
        throw new Exception($"Adding coin failed. Coin jar will exceed max volume.");
      }
      _coins.Add(coin);
    }

    public decimal GetTotalAmount()
    {
      return _coins.Sum(coin => coin.Amount);
    }

    public void Reset()
    {
      _coins.Clear();
    }

    private decimal GetFilledVolume()
    {
      return _coins.Sum(coin => coin.Volume);
    }
  }
}
