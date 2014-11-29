using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDomain.Messaging;

namespace TestMVCApp.Areas.SystemAccessing.Controllers
{
    public class UserController : Controller
    {
        private readonly ICommandBus commandBus;
        //private readonly UserQueries userQueries;

        public ActionResult Index()
        {
            return View();
        }
    }
}