using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExamplePubSub.Domain;

namespace ExamplePubSub.Mnt.Site.Controllers
{
    public class HomeController : Controller
    {
        private ArticleRepository _articleRepository;

        public HomeController()
        {
            _articleRepository = new ArticleRepository();
        }




        public ActionResult Index(string successMessage)
        {
           var articles = _articleRepository.GetAll();

            ViewBag.SucessMessage = successMessage;


            return View(articles);
        }

   
    }
}
