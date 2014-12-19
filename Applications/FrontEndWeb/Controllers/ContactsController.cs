using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FrontEndWeb.Controllers
{
    
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }
    }
}