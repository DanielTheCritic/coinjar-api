using CoinJar.Core.Coins;
using CoinJar.Core.Constants;
using CoinJar.Core.DataModels;
using CoinJar.Core.DataStores;
using CoinJar.Core.Exceptions;
using System.Linq;

namespace CoinJar.Core.CoinJars
{
  public class DefaultCoinJar : ICoinJar
  {
    private readonly decimal _volume;
    private readonly IDataStore _dataStore;

    public DefaultCoinJar(IDataStore dataStore)
    {
      _dataStore = dataStore;
      _volume = CoinJarVolumes.DEFAULT;
    }

    public void AddCoin(ICoin coin)
    {
      if (coin.Volume <= 0)
      {
        throw new CoinJarException(CoinJarErrorCodes.InvalidCoinVolume, $"Adding coin failed. Expected volume must be greater than 0.");
      }
      if (coin.Amount <= 0)
      {
        throw new CoinJarException(CoinJarErrorCodes.InvalidCoinAmount, $"Adding coin failed. Expected amount must be greater than 0.");
      }

      var coinJar = _dataStore.GetCoinJar();

      var filledVolume = coinJar.Coins.Sum(coin => coin.Volume);
      if (filledVolume + coin.Volume > _volume)
      {
        throw new CoinJarException(CoinJarErrorCodes.VolumeExceeded, $"Adding coin failed. Coin jar would exceed max volume of {_volume} fluid ounces.");
      }

      coinJar.Coins.Add(new CoinData { Amount = coin.Amount, Volume = coin.Volume });
      _dataStore.UpdateCoinJar(coinJar);
    }

    public decimal GetTotalAmount()
    {
      return _dataStore.GetCoinJar().Coins.Sum(coin => coin.Amount);
    }

    public void Reset()
    {
      var coinJar = _dataStore.GetCoinJar();
      coinJar.Coins.Clear();
      _dataStore.UpdateCoinJar(coinJar);
    }
  }
}
