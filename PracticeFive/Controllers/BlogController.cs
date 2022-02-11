using PracticeFive.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PracticeFive.Controllers
{
    public class BlogController : Controller
    {
         SaveAnimalsEntities db = new SaveAnimalsEntities();

        // GET: tBlogs
        public ActionResult List()
        {
            var tBlog = db.tBlog.Include(t => t.tMember);
            return View(tBlog.ToList());
        }

        // GET: tBlogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBlog tBlog = db.tBlog.Find(id);
            if (tBlog == null)
            {
                return HttpNotFound();
            }
            return View(tBlog);
        }

        // GET: tBlogs/Create
        public ActionResult Create()
        {
            ViewBag.BlogMemberID = new SelectList(db.tMember, "MemberID", "MemberName");            
            return View();
        }

        // POST: tBlogs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogID,BlogMemberID,BlogCategory,BlogTitle,BlogContent,Created_At")] tBlog tBlog)
        {
            if (ModelState.IsValid)
            {
                tBlog.Created_At = DateTime.Now;
                tBlog.BlogMemberID = Convert.ToInt32(Session["UserID"]);
                db.tBlog.Add(tBlog);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.BlogMemberID = new SelectList(db.tMember, "MemberID", "MemberName", tBlog.BlogMemberID);
            return View(tBlog);
        }

        // GET: tBlogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBlog tBlog = db.tBlog.Find(id);
            if (tBlog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogMemberID = new SelectList(db.tMember, "MemberID", "MemberName", tBlog.BlogMemberID);
           

            return View(tBlog);
        }

        // POST: tBlogs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogID,BlogMemberID,BlogCategory,BlogTitle,BlogContent,Created_At")] tBlog tBlog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.BlogMemberID = new SelectList(db.tMember, "MemberID", "MemberName", tBlog.BlogMemberID);
            return View(tBlog);
        }

        // GET: tBlogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBlog tBlog = db.tBlog.Find(id);
            if (tBlog == null)
            {
                return HttpNotFound();
            }
            return View(tBlog);
        }

        // POST: tBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tBlog tBlog = db.tBlog.Find(id);
            db.tBlog.Remove(tBlog);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}