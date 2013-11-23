using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using ExamplePubSub.Domain;

using Newtonsoft.Json;

using ServiceStack.Redis;

namespace ExamplePubSub.Mnt.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static RedisClient redisPublisher = new RedisClient(RedisConstants.ServerIp);


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        public static void PublishMessageArticleUpdated(int articleId)
        {
            string message = JsonConvert.SerializeObject(new ArticleUpdateMessage() { ArticleId = articleId });

            redisPublisher.PublishMessage(RedisConstants.ChannelArticle, message );

        }
    }
}