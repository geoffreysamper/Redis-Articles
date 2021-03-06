﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using ExamplePubSub.Domain;

namespace ExamplePubSub.Mnt.Site.Controllers
{
    public class ArticleController : Controller
    {
       private ArticleRepository _articleRepository;

        private ArticleService _articleService;

       public ArticleController()
        {
            _articleRepository = new ArticleRepository();
           
        }

        protected override void Initialize(RequestContext requestContext)
        {
            _articleService = new ArticleService(requestContext.HttpContext,_articleRepository,MvcApplication.ArticleMessageService);
            base.Initialize(requestContext);
        }

        public ActionResult Edit(int id)
        {
            var article = _articleRepository.GetById(id);
            ViewBag.Title = "Edit article" ;


            return View(article);
        }


        public ActionResult Create()
        {
            ViewBag.Title = "Create article";





            return this.View("Edit");
        }


        [HttpPost]
        public ActionResult Create(Article article)
        {
            ViewBag.Title = "Create article";
            if (ModelState.IsValid)
            {
                article.Id = 0;

                _articleService.Save(article);
                return RedirectToAction("Index", "Home", new { successMessage = "Article saved" });
            }

            return this.View("Edit");
        }



        [HttpPost]
        public ActionResult Edit(Article inputModel)
        {
            ViewBag.Title = "Edit article";


            var article = _articleRepository.GetById(inputModel.Id);
            

            if (ModelState.IsValid)
            {
                article.Id = inputModel.Id;
                article.Intro = inputModel.Intro;
                article.Title = inputModel.Title;
                article.Body = inputModel.Body;

                _articleService.Save(article);                
                return RedirectToAction("Index", "Home", new {successMessage = "Article saved" });
            }


            return View(article);
        }



    }
}
