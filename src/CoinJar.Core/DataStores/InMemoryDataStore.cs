using CoinJar.Core.Coins;
using CoinJar.Core.DataModels;
using System.Collections.Generic;

namespace CoinJar.Core.DataStores
{
  public class InMemoryDataStore : IDataStore
  {
    private CoinJarData _coinJar;

    public CoinJarData GetCoinJar()
    {
      if(_coinJar == null)
      {
        _coinJar = new CoinJarData
        {
          Coins = new List<CoinData>()
        };
      }
      return _coinJar;
    }

    public void UpdateCoinJar(CoinJarData coinJar)
    {
      _coinJar = coinJar;
    }
  }
}
