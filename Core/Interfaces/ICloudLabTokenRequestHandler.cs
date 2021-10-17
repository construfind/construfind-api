using System;
using System.Threading.Tasks;
using Core.Adapter;
using Microsoft.Extensions.Configuration;

namespace Core.Interfaces
{
  public interface ICloudLabTokenRequestHandler<T> where T : new()
    {
        String GetToken(IConfiguration _config, CloudLabTokenAdapter prCloudLabTokenAdapter);
    }
}