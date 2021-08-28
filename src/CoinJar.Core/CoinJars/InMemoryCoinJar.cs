using CoinJar.Core.Coins;
using CoinJar.Core.Constants;
using CoinJar.Core.Exceptions;
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
      if(coin.Volume <= 0)
      {
        throw new CoinJarException(CoinJarErrorCodes.InvalidCoinVolume, $"Adding coin failed. Expected volume must be greater than 0.");
      }
      if (coin.Amount <= 0)
      {
        throw new CoinJarException(CoinJarErrorCodes.InvalidCoinAmount, $"Adding coin failed. Expected amount must be greater than 0.");
      }

      var filledVolume = GetFilledVolume();
      if(filledVolume + coin.Volume > _volume)
      {
        throw new CoinJarException(CoinJarErrorCodes.VolumeExceeded, $"Adding coin failed. Coin jar would exceed max volume of {_volume} fluid ounces.");
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
