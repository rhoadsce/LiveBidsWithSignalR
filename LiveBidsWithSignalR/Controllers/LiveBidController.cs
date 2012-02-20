using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiveBidsWithSignalR.Models;

namespace LiveBidsWithSignalR.Controllers
{
    public class LiveBidController : Controller
    {
        private LiveBidContext DB { get; set; }

        public LiveBidController()
        {
            this.DB = new LiveBidContext();
        }
        [HttpGet]
        public ActionResult Index(int id)
        {
            LiveBidItem item = this.DB.LiveBidItems.Single(i => i.LiveBidItemId == id);
            @ViewBag.Name = item.Name;
            @ViewBag.Description = item.Description;

            LiveBid model = new LiveBid();
            model.LiveBidItemId = id;
            return View(model);        
        }
        [HttpPost]
        public ActionResult Bid(LiveBid model)
        {
            try
            {
                model.Username = User.Identity.Name;
                model.BidDateTime = DateTime.Now;
                this.DB.LiveBid.Add(model);
                this.DB.SaveChanges();
                return Json("SUCCESS");
            }
            catch
            {
                return Json("ERROR");
            }
        }
        public ActionResult GetCurrentBid(int id)
        {
            try
            {
                var bids = this.DB.LiveBid.Where(b => b.LiveBidItemId == id);
                decimal bid = 0;
                if (bids.Count() > 0)
                {
                    bid = bids.Max(b => b.Bid);
                }
                dynamic model = new { Bid = bid };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("ERROR");
            }
        }
    }
}
