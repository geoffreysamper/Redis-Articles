using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using ExamplePubSub.Domain;

namespace ExamplePubSub.Pub.Site.Controllers
{
    public class HomeController : Controller
    {
        private ArticleRepository _articleRespository;

        private ArticleService _articleService;

        public HomeController()
        {
           
            _articleRespository = new ArticleRepository();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _articleService = new ArticleService(requestContext.HttpContext, _articleRespository);
            
        }

        public ActionResult Index()
        {
        
            return View(_articleRespository.GetAll().OrderByDescending(x=>x.LastModifiedDate));
        }

        public ActionResult Detail(int id)
        {
            var article = _articleService.GetById(id);

            return this.View(article);
        }


    }
}
