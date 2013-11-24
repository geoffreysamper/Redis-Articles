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

        private ArticleMessagesService _articleMessageService;

        public ArticleService(HttpContextBase httpContext, ArticleRepository articleRepository, ArticleMessagesService articleMessagesService)
        {
            this.HttpContext = httpContext;
            this._articleRepository = articleRepository;
            this._articleMessageService = articleMessagesService;
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

        public void Save(Article article)
        {
            int articleId = article.Id;
            _articleRepository.Save(article);

            if (articleId > 0)
            {
                _articleMessageService.PublishArticle(articleId);
            }
            
        }



    }

   
}
