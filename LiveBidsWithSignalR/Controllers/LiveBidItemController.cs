using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiveBidsWithSignalR.Models;

namespace LiveBidsWithSignalR.Controllers
{
    public class LiveBidItemController : Controller
    {
        private LiveBidContext DB { get; set; }
        public LiveBidItemController()
        {
            this.DB = new LiveBidContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            try
            {
                var list = this.DB.LiveBidItems.ToList();
                List<dynamic> result = new List<dynamic>();
                list.ForEach((i) =>
                {
                    result.Add(new { liveBidItemId = i.LiveBidItemId, name = i.Name, description = i.Description });
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("ERROR");
            }
        }
        [HttpPost]
        public ActionResult Create(LiveBidItem model)
        {
            try
            {
                this.DB.LiveBidItems.Add(model);
                this.DB.SaveChanges();
                return Json("SUCCESS");
            }
            catch
            {
                return Json("ERROR");
            }
        }
        [HttpPost]
        public ActionResult Update(LiveBidItem model)
        {
            try
            {
                this.DB.Entry(model).State = System.Data.EntityState.Modified;
                this.DB.SaveChanges();
                return Json("SUCCESS");
            }
            catch
            {
                return Json("ERROR");
            }
        }
        [HttpPost]
        public ActionResult Delete(LiveBidItem model)
        {
            try
            {
                this.DB.Entry(model).State = System.Data.EntityState.Deleted;
                this.DB.SaveChanges();
                return Json("SUCCESS");
            }
            catch
            {
                return Json("ERROR");
            }
        }
    }
}
