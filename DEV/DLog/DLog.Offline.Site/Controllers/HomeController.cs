using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DLog.Offline.Site.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 顶部
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            return View();
        }

        /// <summary>
        /// 底部
        /// </summary>
        /// <returns></returns>
        public ActionResult Footer()
        {
            return View();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }

        /// <summary>
        /// 工作区
        /// </summary>
        /// <returns></returns>
        public ActionResult Workspace()
        {
            return View();
        }

        public ActionResult Test111()
        {
            return View();
        }
        public ActionResult Test222()
        {
            return View();
        }
        public ActionResult Test333()
        {
            return View();
        }
    }
}
