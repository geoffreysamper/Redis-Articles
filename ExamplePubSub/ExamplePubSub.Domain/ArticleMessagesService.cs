using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Newtonsoft.Json;

using ServiceStack.Redis;

namespace ExamplePubSub.Domain
{
   public class ArticleMessagesService : IDisposable
    {
       private RedisClient _redisclient = new RedisClient(RedisConstants.ServerIp);

       public ArticleMessagesService()
       {}


       public void PublishArticle(int articleId)
       {
           string message = JsonConvert.SerializeObject(new ArticleUpdateMessage() { ArticleId = articleId });

           this._redisclient.PublishMessage(RedisConstants.ChannelArticle, message);
       }

       public void InitializeForSubscription()
       {
           var subscription = _redisclient.CreateSubscription();
           subscription.OnMessage = this.OnMessage;
           subscription.SubscribeToChannels(RedisConstants.ChannelArticle);
       }

       private void OnMessage(string channel, string msg)
       {
           var message = JsonConvert.DeserializeObject<ArticleUpdateMessage>(msg);

           string key = "article" + message.ArticleId.ToString();


           HttpRuntime.Cache.Remove(key);
       }


       public void Dispose()
       {
           this._redisclient.Dispose();
       }
    }
   public static class RedisConstants
   {
       public const string ServerIp = "192.168.1.128";
       public const string ChannelArticle = "articleUpdate";
   }

}
