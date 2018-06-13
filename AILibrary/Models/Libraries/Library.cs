using AILibrary.Models.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Libraries
{
    public class Library
    {
        private long _id;
        private ApplicationUser _possesor;
        private List<BookCopy> _books;

        [Required]
        public ApplicationUser Possesor
        {
            get
            {
                return _possesor;
            }

            set
            {
                _possesor = value;
            }
        }

        public List<BookCopy> Books
        {
            get
            {
                return _books;
            }

            set
            {
                _books = value;
            }
        }

        public long Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public Library()
        { }
    }
}