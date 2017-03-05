using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLayer;

namespace MediaPlugin.Controllers
{
    public class PopularTVShowController : Controller
    {
        /// <summary>
        /// MoviceManager business layer implementation, which is injected by IoC.
        /// </summary>
        public IMoviceManager MoviceManager { get; set; }


        public ActionResult Index(string id = "")
        {
            //
            return View(MoviceManager.GetMovies());
        }

        public ActionResult Delete(string id)
        {
            MoviceManager.DeleteMovie(id);
            return RedirectToAction("Index", new { plugin = "MediaPlugin" });
        }
    }
}
