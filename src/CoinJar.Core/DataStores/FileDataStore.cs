using CoinJar.Core.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CoinJar.Core.DataStores
{
  public class FileDataStore : IDataStore
  {
    private readonly string _filePath;
    private readonly string _directoryPath;

    public FileDataStore()
    {
      _directoryPath = Path.Combine(AppContext.BaseDirectory, "data");
      _filePath = Path.Combine(_directoryPath, "coinjar.json");
    }

    public CoinJarData GetCoinJar()
    {
      using (var stream = CreateFileStream(FileAccess.Read))
      {
        using (var reader = new StreamReader(stream))
        {
          var content = reader.ReadToEnd();
          if (string.IsNullOrEmpty(content))
          {
            return new CoinJarData
            {
              Coins = new List<CoinData>()
            };
          }
          return JsonSerializer.Deserialize<CoinJarData>(content);
        }
      }
    }

    public void UpdateCoinJar(CoinJarData coinJar)
    {
      using (var stream = CreateFileStream(FileAccess.Write))
      {
        using (var writer = new StreamWriter(stream))
        {
          writer.Write(JsonSerializer.Serialize(coinJar));
        }
      }
    }

    private FileStream CreateFileStream(FileAccess access)
    {
      if(!Directory.Exists(_directoryPath))
      {
        Directory.CreateDirectory(_directoryPath);
      }
      return new FileStream(_filePath, FileMode.OpenOrCreate, access);
    }
  }
}
