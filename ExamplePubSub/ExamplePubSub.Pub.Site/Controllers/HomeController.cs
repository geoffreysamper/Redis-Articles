using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExamplePubSub.Domain;

namespace ExamplePubSub.Pub.Site.Controllers
{
    public class HomeController : Controller
    {
        private ArticleRepository _articleRespository= new ArticleRepository();

        public ActionResult Index()
        {
        
            return View(_articleRespository.GetAll().OrderByDescending(x=>x.LastModifiedDate));
        }

        public ActionResult Detail(int id)
        {
          var article =   _articleRespository.GetById(id);

            return this.View(article);
        }


    }
}
