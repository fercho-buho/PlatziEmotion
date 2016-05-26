using EmotionPlatzi.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.welcomeMessage = "Hola mundote";
            ViewBag.Entero = 10;
            return View();
        }
        public ActionResult IndexAlt()
        {
            var modelo = new Home();
            modelo.WelcomeMessage = "Hola mundo desde el modelo";
            return View(modelo);
        }
    }
}