using AILibrary.Models.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Books
{
    public class BookCopy
    {
        private long _id;
        private Book _book;
        private ApplicationUser _possesor;
        private ApplicationUser _currentlyPossesdByUser;
        private Library _library;
        private long _amountOfPages;

        [Required]
        [DisplayName("Book")]
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
        [DisplayName("Owner")]
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

        [DisplayName("Currently possesed by")]
        public ApplicationUser CurrentlyPossesdByUser
        {
            get
            {
                return _currentlyPossesdByUser;
            }

            set
            {
                _currentlyPossesdByUser = value;
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

        [Required]
        [DisplayName("Amount of pages")]
        public long AmountOfPages
        {
            get
            {
                return _amountOfPages;
            }

            set
            {
                _amountOfPages = value;
            }
        }

        public BookCopy()
        { }
    }
}