using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using ExamplePubSub.Domain;

using Newtonsoft.Json;

using ServiceStack.Redis;

namespace ExamplePubSub.Pub.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private RedisClient redisConsumer = new RedisClient(RedisConstants.ServerIp);


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            var subscription  = redisConsumer.CreateSubscription();

            subscription.OnMessage = (channel, msg) =>
            {
                var message = JsonConvert.DeserializeObject<ArticleUpdateMessage>(msg);
               
                string key = "article" + message.ArticleId.ToString();


                HttpRuntime.Cache.Remove(key);



            };

            ThreadPool.QueueUserWorkItem(
                (s) => subscription.SubscribeToChannels(RedisConstants.ChannelArticle));
            


        }
    }
}