using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AILibrary.Models;
using AILibrary.Models.Permissions;
using AILibrary.Models.Libraries;
using Microsoft.AspNet.Identity;

namespace AILibrary.Controllers
{
    public class PermissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public static string UsersDDLElements = "UsersList";
        public static string UsersDDLOnFormName = "UsersDDL";

        // GET: Permissions
        [Authorize]
        public ActionResult Index(string sortOrder, string emailSearchString)
        {
            ViewBag.MailSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "mail_desc" : "";

            var permissions = PermissionsWithIncludes()
                .ToList()
                .Where(p => string.IsNullOrWhiteSpace(emailSearchString) || p.User.Email.ToLower().Contains(emailSearchString.ToLower()))
                .Select(p => p);

            switch (sortOrder)
            {
                case "mail_desc":
                    permissions = permissions.OrderByDescending(b => b.User.Email);
                    break;
                default:
                    permissions = permissions.OrderBy(b => b.User.Email);
                    break;
            }

            return View(permissions);
        }

        // GET: Permissions/Create
        [Authorize]
        public ActionResult Create()
        {
            AddToViewDataPrepareUserDropDownList();
            return View();
        }

        // POST: Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id")] Permission permission)
        {
            SetUser(permission);
            SetLibrary(permission);

            if (ModelState.IsValid)
            {
                db.Permissions.Add(permission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permission);
        }

        // GET: Permissions/Delete/5
        [Authorize]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = PermissionsWithIncludes().First(p => p.Id.Equals(id));
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(long id)
        {
            Permission permission = PermissionsWithIncludes().First(p => p.Id.Equals(id));
            db.Permissions.Remove(permission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Library GetCurretUsersLibrary()
        {
            var libraryId = GetCurrentUser().Id;
            return db.Libraries.Include(l => l.Possesor).First(l => l.Possesor.Id.Equals(libraryId));
        }

        private ApplicationUser GetCurrentUser()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return db.Users.Find(userId);
        }

        private IList<Permission> PermissionsWithIncludes()
        {
            var libraryId = GetCurretUsersLibrary().Id;
            return db.Permissions
                .Include(p => p.Library)
                .Include(p => p.User)
                .Where(p => p.Library.Id.Equals(libraryId) && p.User.EmailConfirmed)
                .ToList();
        }

        private IList<SelectListItem> PrepareUsersSelect()
        {
            var currentUserID = GetCurrentUser().Id;
            var usersWithPermisions = UserIdsWithPermisions();
            return db.Users
                .Where(u => !u.Id.Equals(currentUserID) && !usersWithPermisions.Contains(u.Id))
                .Select(u => new SelectListItem() { Text = u.Email, Value = u.Email })
                .ToList();
        }

        private IList<string> UserIdsWithPermisions()
        {
            var currentLibraryID = GetCurretUsersLibrary().Id;
            return db.Permissions
                .Include(p => p.Library)
                .Where(p => p.Library.Id.Equals(currentLibraryID))
                .Include(p => p.User)
                .Select(p => p.User.Id)
                .ToList();
        }

        private void AddToViewDataPrepareUserDropDownList()
        {
            ViewData[UsersDDLElements] = PrepareUsersSelect();
        }

        private void SetUser(Permission p)
        {
            string userId = Request.Form[UsersDDLOnFormName];
            p.User = db.Users.First(u => u.Email.Equals(userId));
        }

        private void SetLibrary(Permission p)
        {
            p.Library = GetCurretUsersLibrary();
        }
    }
}
