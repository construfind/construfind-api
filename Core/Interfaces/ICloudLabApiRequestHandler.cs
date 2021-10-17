using RestSharp;

namespace Core.Interfaces
{
  public interface ICloudLabApiRequestHandler<T> where T : new()
    {
        IRestResponse GetRequest(string prSegmentos, string prScope);
        IRestResponse PostRequest(string prSegmentos, string prScope, T prBody);
    }
}