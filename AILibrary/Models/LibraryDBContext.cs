using AILibrary.Models.Books;
using AILibrary.Models.Libraries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AILibrary.Models
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Library> Libraries { get; set; }
    }
}