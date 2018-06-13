using AILibrary.Models.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Books
{
    public class BookCopy
    {
        private long _id;
        private Book _book;
        private string _possesorId;
        private string _currentlyPossesdByUserId;
        private Library _library;

        [Required]
        public Book Book
        {
            get
            {
                return _book;
            }

            set
            {
                _book = value;
            }
        }

        [Required]
        public string PossesorId
        {
            get
            {
                return _possesorId;
            }

            set
            {
                _possesorId = value;
            }
        }

        public string CurrentlyPossesdByUserId
        {
            get
            {
                return _currentlyPossesdByUserId;
            }

            set
            {
                _currentlyPossesdByUserId = value;
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

        public Library Library
        {
            get
            {
                return _library;
            }

            set
            {
                _library = value;
            }
        }

        public BookCopy(Book _book, string _possesorId, string _currentlyPossesdByUserId)
        {
            this.Book = _book;
            this.PossesorId = _possesorId;
            this.CurrentlyPossesdByUserId = _currentlyPossesdByUserId;
        }

        public BookCopy()
        { }
    }
}