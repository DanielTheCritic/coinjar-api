using CoinJar.Core.Coins;
using CoinJar.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinJar.Core.DataStores
{
  public interface IDataStore
  {
    CoinJarData GetCoinJar();

    void UpdateCoinJar(CoinJarData coinJar);
  }
}
