using RestSharp;

namespace Core.Interfaces
{
  public interface IFCMPushNotificationHandler<T> where T : new()
    {
        IRestResponse SendPushNotification(T prBody);
    }
}