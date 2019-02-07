using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ticketTracker.Controllers
{
    public class testViewController : Controller
    {
        // GET: testView
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult testView()
        {
            return View();
        }
        public ActionResult testView2()
        {
            return View();
        }
        public ActionResult testHide()
        {
            if(7 == 7)
            {
                return View();
            }

        }
        public ActionResult testNumerableView()
        {
            return View();
        }
    }
}