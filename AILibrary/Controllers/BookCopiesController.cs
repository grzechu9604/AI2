using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AILibrary.Models;
using AILibrary.Models.Books;
using AILibrary.Models.Libraries;

namespace AILibrary.Controllers
{
    public class BookCopiesController : Controller
    {
        public static string BooksDDLElements = "BooksList";
        public static string BooksDDLOnFormName = "BooksDDL";

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser GetCurrentUser()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return  db.Users.Find(userId);
        }
        
        private IQueryable<SelectListItem> BooksSelectElements() => db.Books.Select(book => new SelectListItem { Text = book.AuthorName + " " + book.Title, Value = book.Id.ToString() });


        // GET: BookCopies
        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.TitleSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParam = sortOrder == "author" ? "author_desc" : "author";
            ViewBag.PagesSortParam = sortOrder == "page" ? "page_desc" : "page";
            ViewBag.PossesorSortParam = sortOrder == "possesor" ? "possesor_desc" : "possesor";
            ViewBag.CurrentlyPossesed = sortOrder == "CurrentlyPossesed" ? "CurrentlyPossesed_desc" : "CurrentlyPossesed";

            var bookCopies = BookCopiesWithIncludes().Select(b => b);

            switch (sortOrder)
            {
                case "title_desc":
                    bookCopies = bookCopies.OrderByDescending(b => b.Book.Title);
                    break;
                case "author":
                    bookCopies = bookCopies.OrderBy(b => b.Book.AuthorName);
                    break;
                case "author_desc":
                    bookCopies = bookCopies.OrderByDescending(b => b.Book.AuthorName);
                    break;
                case "page":
                    bookCopies = bookCopies.OrderBy(b => b.AmountOfPages);
                    break;
                case "page_desc":
                    bookCopies = bookCopies.OrderByDescending(b => b.AmountOfPages);
                    break;
                case "possesor":
                    bookCopies = bookCopies.OrderBy(b => b.Possesor.Email);
                    break;
                case "possesor_desc":
                    bookCopies = bookCopies.OrderByDescending(b => b.Possesor.Email);
                    break;
                case "CurrentlyPossesed":
                    bookCopies = bookCopies.OrderBy(b => b.CurrentlyPossesdByUser.Email);
                    break;
                case "CurrentlyPossesed_desc":
                    bookCopies = bookCopies.OrderByDescending(b => b.CurrentlyPossesdByUser.Email);
                    break;
                default:
                    bookCopies = bookCopies.OrderBy(b => b.Book.Title);
                    break;
            }
            return View(bookCopies);
        }

        // GET: BookCopies/Details/5
        [Authorize]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = BookCopiesWithIncludes().First(b => b.Id == id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            AddToViewDataPreparedBooksDropDownList(bookCopy);
            return View(bookCopy);
        }

        // GET: BookCopies/Create
        [Authorize]
        public ActionResult Create()
        {
            AddToViewDataPreparedBooksDropDownList(null);
            return View();
        }

        // POST: BookCopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,AmountOfPages")] BookCopy bookCopy)
        {
            SetBook(bookCopy);
            SetOwnerData(bookCopy);
            
            
            if (ModelState.IsValid)
            {
                db.BookCopies.Add(bookCopy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AddToViewDataPreparedBooksDropDownList(bookCopy);
            return View(bookCopy);
        }

        // GET: BookCopies/Edit/5
        [Authorize]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = BookCopiesWithIncludes().First(b => b.Id == id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // POST: BookCopies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,PossesorId,CurrentlyPossesdByUserId,AmountOfPages")] BookCopy bookCopy)
        {
            var editingEntry = BookCopiesWithIncludes().First(e => e.Id == bookCopy.Id);
            editingEntry.AmountOfPages = bookCopy.AmountOfPages;
            ClearModelState();

            if (ModelState.IsValid)
            {
                db.Entry(editingEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        [Authorize]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = BookCopiesWithIncludes().First(b => b.Id == id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            AddToViewDataPreparedBooksDropDownList(bookCopy);
            return View(bookCopy);
        }

        // POST: BookCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(long id)
        {
            BookCopy bookCopy = db.BookCopies.Find(id);
            db.BookCopies.Remove(bookCopy);
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

        private void AddToViewDataPreparedBooksDropDownList(BookCopy bc)
        {
            var selectList = BooksSelectElements().ToList();
            var selectedBook = selectList.FirstOrDefault(book => book.Value.Equals(bc?.Book?.Id.ToString()));
            if (selectedBook!=null)
            {
                selectedBook.Selected = true;
            }
            ViewData[BooksDDLElements] = selectList;
        }

        private void SetOwnerData(BookCopy b)
        {
            b.Possesor = GetCurrentUser();
            b.Library = db.Libraries.FirstOrDefault(l => l.Possesor.Id == b.Possesor.Id);
            ModelState.Remove("Possesor");
        }

        private void SetBook(BookCopy b)
        {
            long bookId = Convert.ToInt64(Request.Form[BooksDDLOnFormName]);
            b.Book = db.Books.Find(bookId);
            ModelState.Remove("Book");
        }

        private void RestoreBook(BookCopy b)
        {
            ApplicationDbContext duplicateContext = new ApplicationDbContext();
            var bookId = duplicateContext.BookCopies.Include(bookCopy => bookCopy.Book).First(bookCopy => bookCopy.Id.Equals(b.Id)).Book.Id;
            duplicateContext.Dispose();
            b.Book = db.Books.Find(bookId);
            ModelState.Remove("Book");
        }

        private IQueryable<BookCopy> BookCopiesWithIncludes()
        {
            var librariesWithPrivileges = LibrariesWithPrivileges();
            return db.BookCopies
                .Include(b => b.Library)
                .Where(b => librariesWithPrivileges.Contains(b.Library.Id))
                .Include(b => b.Book)
                .Include(b => b.CurrentlyPossesdByUser)
                .Include(b => b.Possesor);
        }

        private void ClearModelState()
        {
            ModelState.Remove("Book");
            ModelState.Remove("Possesor");
        }

        private IList<long> LibrariesWithPrivileges()
        {
            var userId = GetCurrentUser().Id;
            var librariesWithPermission = db.Permissions
                .Include(p => p.Library)
                .Include(p => p.User)
                .Where(p => p.User.Id.Equals(userId))
                .Select(p => p.Library.Id)
                .ToList();

            librariesWithPermission.Add(GetCurretUsersLibrary().Id);
            return librariesWithPermission;
        }

        private Library GetCurretUsersLibrary()
        {
            var libraryId = GetCurrentUser().Id;
            return db.Libraries.Include(l => l.Possesor).First(l => l.Possesor.Id.Equals(libraryId));
        }
    }
}
