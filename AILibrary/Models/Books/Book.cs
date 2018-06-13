using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Books
{
    public class Book
    {
        private long _id;
        private string _authorName;
        private string _title;

        public Book(string _authorName, string _title)
        {
            this.AuthorName = _authorName;
            this.Title = _title;
        }

        public string AuthorName
        {
            get
            {
                return _authorName;
            }

            set
            {
                _authorName = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
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
    }
}