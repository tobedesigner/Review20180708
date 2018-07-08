using Review20180708.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Review20180708.Controllers
{
    public abstract class BaseController : Controller
    {
        public IUnitOfWork unitOfWork = RepositoryHelper.GetUnitOfWork();

        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("ErrorPage", "Clients").ExecuteResult(this.ControllerContext);
        }
    }
}