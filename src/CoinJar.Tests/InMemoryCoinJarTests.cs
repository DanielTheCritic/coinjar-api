using CoinJar.Core.CoinJars;
using CoinJar.Core.Constants;
using CoinJar.Core.Exceptions;
using NUnit.Framework;
using System;

namespace CoinJar.Tests
{
  [TestFixture]
  public class InMemoryCoinJarTests
  {
    private InMemoryCoinJar GetCoinJar()
    {
      return new InMemoryCoinJar();
    }

    private MockCoin CreateCoin(decimal amount, decimal volume)
    {
      return new MockCoin(amount, volume);
    }

    [TestCase(-10)]
    [TestCase(0)]
    public void AddCoin_GivenInvalidAmount_ShouldThrowException(decimal amount)
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(amount, 12);

      // Act
      TestDelegate action = () => sut.AddCoin(coin);

      // Assert
      var exception = Assert.Throws<CoinJarException>(action);
      Assert.AreEqual(CoinJarErrorCodes.InvalidCoinAmount, exception.ErrorCode);
    }

    [TestCase(-5)]
    [TestCase(0)]
    public void AddCoin_GivenInvalidVolume_ShouldThrowException(decimal volume)
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(15, volume);

      // Act
      TestDelegate action = () => sut.AddCoin(coin);

      // Assert
      var exception = Assert.Throws<CoinJarException>(action);
      Assert.AreEqual(CoinJarErrorCodes.InvalidCoinVolume, exception.ErrorCode);
    }

    // Coin jar volume is 42.
    [TestCase(55)]
    [TestCase(66)]
    public void AddCoin_VolumeThatExceedsCoinJarVolume_ShouldThrowException(decimal volume)
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(15, volume);

      // Act
      TestDelegate action = () => sut.AddCoin(coin);

      // Assert
      var exception = Assert.Throws<CoinJarException>(action);
      Assert.AreEqual(CoinJarErrorCodes.VolumeExceeded, exception.ErrorCode);
    }

    [Test]
    public void AddCoin_GivenMultipleCoinsUntilVolumeExceeded_ShouldThrowException()
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(15, 20);

      // Act
      sut.AddCoin(coin); // Total volume = 20;
      sut.AddCoin(coin); // Total volume = 40;
      TestDelegate action = () => sut.AddCoin(coin);

      // Assert
      var exception = Assert.Throws<CoinJarException>(action);
      Assert.AreEqual(CoinJarErrorCodes.VolumeExceeded, exception.ErrorCode);
    }

    [Test]
    public void AddCoin_GivenMultipleCoinsUntilVolumeExceeded_ShouldNotUpdateAmount()
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(15, 20);

      // Act
      sut.AddCoin(coin); // Total volume = 20; Total amount = 15;
      sut.AddCoin(coin); // Total volume = 40; Total amount = 30;
      try
      {
        sut.AddCoin(coin);
      }
      catch (CoinJarException)
      { }

      var amount = sut.GetTotalAmount();

      // Assert
      Assert.AreEqual(30, amount);
    }

    [Test]
    public void AddCoin_GivenValidCoin_ShouldSucceed()
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(15, 20);

      // Act
      TestDelegate action = () => sut.AddCoin(coin);

      // Assert
      Assert.DoesNotThrow(action);
    }

    [Test]
    public void AddCoin_GivenValidCoin_ShouldUpdateAmount()
    {
      // Arrange
      var sut = GetCoinJar();
      var coin = CreateCoin(15, 20);

      // Act
      sut.AddCoin(coin);
      var amount = sut.GetTotalAmount();

      // Assert
      Assert.AreEqual(15, amount);
    }

    [Test]
    public void GetTotalAmount_GivenNoCoins_ShouldReturnZero()
    {
      // Arrange
      var sut = GetCoinJar();

      // Act
      var amount = sut.GetTotalAmount();

      // Assert
      Assert.AreEqual(0, amount);
    }

    [Test]
    public void GetTotalAmount_GivenAddedCoin_ShouldReturnCoinAmount()
    {
      // Arrange
      var sut = GetCoinJar();
      sut.AddCoin(CreateCoin(15, 20));

      // Act
      var amount = sut.GetTotalAmount();

      // Assert
      Assert.AreEqual(15, amount);
    }

    [Test]
    public void GetTotalAmount_GivenMultipleCoins_ShouldReturnSum()
    {
      // Arrange
      var sut = GetCoinJar();
      sut.AddCoin(CreateCoin(15, 20));
      sut.AddCoin(CreateCoin(13, 20));

      // Act
      var amount = sut.GetTotalAmount();

      // Assert
      Assert.AreEqual(28, amount);
    }

    [Test]
    public void Reset_GivenAddedCoins_ShouldClearAmount()
    {
      // Arrange
      var sut = GetCoinJar();
      sut.AddCoin(CreateCoin(15, 20));
      sut.AddCoin(CreateCoin(13, 20));

      // Act

      var amount = sut.GetTotalAmount();
      sut.Reset();
      var amountAfterReset = sut.GetTotalAmount();

      // Assert
      Assert.AreEqual(28, amount);
      Assert.AreEqual(0, amountAfterReset);
    }
  }
}
