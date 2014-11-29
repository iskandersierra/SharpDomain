using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVCApp.Areas.UserManagement.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: UserManagement/UserRole
        public ActionResult Index()
        {
            return View();
        }
    }
}