using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Books
{
    public class Book
    {
        private long _id;
        private string _authorName;
        private string _title;

        public Book()
        { }

        public Book(string _authorName, string _title)
        {
            this.AuthorName = _authorName;
            this.Title = _title;
        }

        [Required]
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

        [Required]
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

        public override bool Equals(object o)
        {
            return o != null && o is Book 
                && (o as Book).AuthorName.Equals(AuthorName) 
                && (o as Book).Title.Equals(Title);
        }
    }
}