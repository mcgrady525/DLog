using DLog.Offline.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLog.Entity.ViewModel;
using DLog.IService;
using System.ServiceModel;
using Webdiyer.WebControls.Mvc;
using DLog.Entity;

namespace DLog.Offline.Site.Controllers
{
    public class MenuController : BaseController
    {
        public ActionResult ModifyConfig()
        {
            return View();
        }

        public ActionResult ModifyPassword()
        {
            return View();
        }

        #region 调试日志
        /// <summary>
        /// 调试日志列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult DebugLogList(DebugLogListModel model, int pageIndex = 1)
        {
            if (model == null)
            {
                model = new DebugLogListModel();
            }
            if (model.Request == null)
            {
                model.Request = new SearchDebugLogListRQ();
            }
            model.Request.PageIndex = pageIndex;

            //设定StartTime和EndTime
            model.Request.StartTime = model.Request.StartTime ?? DateTime.Now.Date;
            model.Request.EndTime = model.Request.EndTime ?? DateTime.Now.Date.AddHours(24);

            using (var factory = new ChannelFactory<IDLogDebugLogService>("*"))
            {
                var client = factory.CreateChannel();
                var result = client.SearchDebugLogList(model.Request);
                model.QueryResult = new PagedList<DebugLog>(result.Entities, result.PageIndex, result.PageSize, result.TotalCount);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_DebugLogList", model.QueryResult);
                }

                return View(model);
            }
        }

        /// <summary>
        /// 调试日志详情
        /// </summary>
        /// <param name="id">调试日志id</param>
        /// <returns></returns>
        public ActionResult DebugLogDetail(long id)
        {
            DebugLog model = new DebugLog();

            using (var factory = new ChannelFactory<IDLogDebugLogService>("*"))
            {
                var client = factory.CreateChannel();
                model = client.SearchDebugLogByID(id).Content;
            }

            return View(model);
        }
        #endregion

        #region 错误日志
        /// <summary>
        /// 错误日志列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorLogList(ErrorLogListModel model, int pageIndex = 1)
        {
            if (model == null)
            {
                model = new ErrorLogListModel();
            }
            if (model.Request == null)
            {
                model.Request = new SearchErrorLogListRQ();
            }
            model.Request.PageIndex = pageIndex;

            //设定StartTime和EndTime
            model.Request.StartTime = model.Request.StartTime ?? DateTime.Now.Date;
            model.Request.EndTime = model.Request.EndTime ?? DateTime.Now.Date.AddHours(24);

            using (var factory = new ChannelFactory<IDLogErrorLogService>("*"))
            {
                var client = factory.CreateChannel();
                var result = client.SearchErrorLogList(model.Request);
                model.QueryResult = new PagedList<ErrorLog>(result.Entities, result.PageIndex, result.PageSize, result.TotalCount);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ErrorLogList", model.QueryResult);
                }

                return View(model);
            }
        }

        /// <summary>
        /// 错误日志详情页
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        public ActionResult ErrorLogDetail(long id)
        {
            ErrorLog model = new ErrorLog();

            using (var factory = new ChannelFactory<IDLogErrorLogService>("*"))
            {
                var client = factory.CreateChannel();
                model = client.SearchErrorLogByID(id).Content;
            }

            return View(model);
        }
        #endregion

        #region 性能日志
        /// <summary>
        /// 性能日志列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult PerfLogList(PerfLogListModel model, int pageIndex = 1)
        {
            if (model == null)
            {
                model = new PerfLogListModel();
            }
            if (model.Request == null)
            {
                model.Request = new SearchPerfLogListRQ();
            }
            model.Request.PageIndex = pageIndex;

            //设定StartTime和EndTime
            model.Request.StartTime = model.Request.StartTime ?? DateTime.Now.Date;
            model.Request.EndTime = model.Request.EndTime ?? DateTime.Now.Date.AddHours(24);

            using (var factory = new ChannelFactory<IDLogPerfLogService>("*"))
            {
                var client = factory.CreateChannel();
                var result = client.SearchPerfLogList(model.Request);
                model.QueryResult = new PagedList<PerfLog>(result.Entities, result.PageIndex, result.PageSize, result.TotalCount);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_PerfLogList", model.QueryResult);
                }

                return View(model);
            }
        }

        /// <summary>
        /// 性能日志详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PerfLogDetail(long id)
        {
            PerfLog model = new PerfLog();

            using (var factory = new ChannelFactory<IDLogPerfLogService>("*"))
            {
                var client = factory.CreateChannel();
                model = client.SearchPerfLogByID(id).Content;
            }

            return View(model);
        } 
        #endregion

    }
}
