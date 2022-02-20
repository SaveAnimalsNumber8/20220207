using PracticeFive.Models;
using PracticeFive.ViewModel;
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
        public ActionResult List(string type)
        {
            List<int> likeList = new List<int>();
            if (Session["UserID"] != null)
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                likeList = db.tBlog.Join(db.CollectBlog.Where(x => x.CollectMemberID == userid),
                    x => x.BlogID,
                    c => c.CollectBlogID,
                    (x, c) => x.BlogID).ToList();
            }

            string keyword = Request.Form["Keyword"];
            Console.WriteLine(keyword);

            var tBlog = db.tBlog.Include(t => t.tMember);



            if (!string.IsNullOrEmpty(type))
            {
                tBlog = tBlog.Where(x => x.BlogCategory == type);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                tBlog = tBlog.Where(x => x.BlogCategory.Contains(keyword) ||
                x.BlogTitle.Contains(keyword) ||
                x.BlogContent.Contains(keyword) ||
                x.tMember.MemberName.Contains(keyword));
            }


            IQueryable<BlogListViewModel> resourtList = tBlog.Select(x => new BlogListViewModel
            {
                Created_At = x.Created_At,
                BlogCategory = x.BlogCategory,
                BlogContent = x.BlogContent,
                BlogMemberID = x.BlogMemberID,
                BlogTitle = x.BlogTitle,
                BlogID = x.BlogID,
                tMember = x.tMember,
                likes = likeList.FirstOrDefault(c => c == x.BlogID) == 0 ? false : true

            });

            return View(resourtList.OrderByDescending(x => x.Created_At).ToList());
        }

        // GET: tBlogs/Details/5
        public ActionResult Details(int? id, string type)
        {
            BlogDetailViewModel tBlog = new BlogDetailViewModel();
            if (type == "Add")
            {
                tBlog.BlogMemberName = Session["UserName"].ToString();
                tBlog.BlogMemberID = Convert.ToInt32(Session["UserID"]);
            }
            else if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                tBlog = db.tBlog.Include(t => t.tMember).Where(x=>x.BlogID == id).Select(x=>new BlogDetailViewModel {
                    BlogMemberName = x.tMember.MemberName,
                    BlogMemberID = x.BlogMemberID,
                    Created_At = x.Created_At,
                    BlogCategory = x.BlogCategory,
                    BlogContent = x.BlogContent,
                    BlogTitle = x.BlogTitle,
                }).FirstOrDefault();
                if (tBlog == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.action = type;
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
        public ActionResult Create(tBlog tBlog)
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

        public ActionResult Delete(int id)
        {
            tBlog tBlog = db.tBlog.Find(id);
            db.tBlog.Remove(tBlog);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult AddCollectBlog(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            tBlog AddCollect = db.tBlog.FirstOrDefault(x => x.BlogID == id);

            int MemberID = Convert.ToInt32(Session["UserID"]);
            //int BlogID = AddCollect.BlogID;

            var Collect = db.CollectBlog
                .Where(c => c.CollectMemberID == MemberID && c.CollectBlogID == AddCollect.BlogID)
                .FirstOrDefault();


            if (AddCollect != null)
            {
                if (Collect == null)
                {
                    db.CollectBlog.Add(new CollectBlog() { CollectMemberID = MemberID, CollectBlogID = AddCollect.BlogID });
                    db.SaveChanges();
                }
                else
                {
                    db.CollectBlog.Remove(Collect);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("List");
        }
    }
}