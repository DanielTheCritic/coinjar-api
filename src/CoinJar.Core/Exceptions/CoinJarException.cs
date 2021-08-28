using System;
using System.Collections.Generic;
using System.Text;

namespace CoinJar.Core.Exceptions
{
  public class CoinJarException : Exception
  {
    public CoinJarException(int errorCode, string message) : base(message)
    {
      ErrorCode = errorCode;
    }

    public int ErrorCode { get; }
  }
}
