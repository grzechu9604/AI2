using AILibrary.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Libraries
{
    public class Library
    {
        private long _id;
        private string _possesorId;
        private List<BookCopy> _books;

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

        public Library(string _possesorId)
        {
            this.PossesorId = _possesorId;
            this.Books = new List<BookCopy>();
        }
    }
}