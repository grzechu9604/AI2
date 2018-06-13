using System;
using System.Collections.Generic;
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

        public BookCopy(Book _book, string _possesorId, string _currentlyPossesdByUserId)
        {
            this.Book = _book;
            this.PossesorId = _possesorId;
            this.CurrentlyPossesdByUserId = _currentlyPossesdByUserId;
        }
    }
}