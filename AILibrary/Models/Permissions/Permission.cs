using AILibrary.Models.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AILibrary.Models.Permissions
{
    public class Permission
    {
        private long _id;
        private Library _library;
        private ApplicationUser _user;

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

        public ApplicationUser User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }
    }
}