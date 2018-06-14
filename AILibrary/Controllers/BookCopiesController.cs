﻿using System;
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

        //private public List<Book> BooksSelectElements() => db.Books.Select(book => new SelectListItem { Text = book.AuthorName + " " + book.Title, Value = book.Id.ToString()});

        private IQueryable<SelectListItem> BooksSelectElements() => db.Books.Select(book => new SelectListItem { Text = book.AuthorName + " " + book.Title, Value = book.Id.ToString() });


        // GET: BookCopies
        public ActionResult Index()
        {
            return View(BookCopiesWithIncludes());
        }

        // GET: BookCopies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            AddToViewDataPreparedBooksDropDownList(bookCopy);
            return View(bookCopy);
        }

        // GET: BookCopies/Create
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
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,PossesorId,CurrentlyPossesdByUserId,AmountOfPages")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCopy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AddToViewDataPreparedBooksDropDownList(bookCopy);
            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
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

        private IQueryable<BookCopy> BookCopiesWithIncludes()
        {
            return db.BookCopies
                .Include(b => b.Book)
                .Include(b => b.CurrentlyPossesdByUser)
                .Include(b => b.Possesor);
        }
    }
}
