using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using ServiceStack.OrmLite;

namespace ExamplePubSub.Domain
{
    public class ArticleRepository
    {
        private string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

        }



        public Article GetById(int articleId)
        {
            OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory(ConnectionString, SqlServerDialect.Provider);
            using (var db = factory.OpenDbConnection())
            {
                return db.SelectParam<Article>(x => x.Id == articleId).FirstOrDefault();
            }


        }

        public List<Article> GetAll()
        {
            OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory(ConnectionString, SqlServerDialect.Provider);
            using (var db = factory.OpenDbConnection())
            {
                return db.Select<Article>();
            }
        }

        public void Save(Article article)
        {
            OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory(ConnectionString, SqlServerDialect.Provider);

            article.LastModifiedDate = DateTime.Now;
            using (var db = factory.OpenDbConnection())
            {
                if (article.Id <= 0)
                {
                    article.Id = (int)db.InsertParam(article, true);
                }
                else
                {
                    db.UpdateParam(article);
                }

            }



        }
    }
}
