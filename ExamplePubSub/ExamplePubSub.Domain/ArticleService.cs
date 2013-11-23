using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ExamplePubSub.Domain
{
    public class ArticleService
    {
        private System.Web.HttpContextBase HttpContext;

        private ArticleRepository _articleRepository;

        public ArticleService(HttpContextBase httpContext, ArticleRepository articleRepository)
        {
            this.HttpContext = httpContext;
            this._articleRepository = articleRepository;
        }

        public Article GetById(int id)
        {
            string key = "article" + id.ToString();

            var article = (Article)HttpContext.Cache.Get(key);
            if (article == null)
            {
                article = this._articleRepository.GetById(id);
                HttpContext.Cache.Insert(key, article, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }

            return article;

        }



    }

    public static class RedisConstants
    {
        public const string ServerIp = "192.168.1.128";

        public const string ChannelArticle = "articleUpdate";
    }

}
