using PracticeFive.Models;
using PracticeFive.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace PracticeFive.Controllers
{
    public class TraceListMemberController : Controller
    {
        SaveAnimalsEntities sadb = new SaveAnimalsEntities();
        // GET: TraceListMember
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult rescueTracelist()
        {
            var TraceList=from m in sadb.FollowRescue select m;
            return View(TraceList); 
            //if (true)
            //{

            //}
            //IEnumerable<TracelistViewModel> track = sadb.FollowRescue.AsEnumerable()
            //       .Where(x => x.FollowMemberID == Convert.ToInt32(Session["UserID"]))
            //       .Select(x => new TracelistViewModel()
            //       {
            //           RescueID = x.FollowResue,
            //           RescueEvent = x.tRescue.RescueEvent
            //       });
            //return View(track); 
        }
        public ActionResult DeleteFollow(int id)
        {
            FollowRescue followDelete = sadb.FollowRescue.FirstOrDefault(p => p.FollowID == id);
            if (followDelete != null)
            {
                sadb.FollowRescue.Remove(followDelete);
                sadb.SaveChanges();
            }
            return RedirectToAction("TraceList");
        }
    }
}