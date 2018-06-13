using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AILibrary.Views.Helpers
{
    public class TextElements
    {
        public static string MailConfirmationMessage = "Please check Your email and cofirm it. You must confirm it before you log in.";
        public static string MailConfirmationTitle = "Confirm your email";
        public static string MailConfirmationHeader = "Thank you";

        public static string AlreadyVeryfiedMailTitle = "Mail already verified";
        public static string AlreadyVeryfiedMailHeader = "Mail already verified";
        public static string AlreadyVeryfiedMailMessage = "Your mail has been already verified";

        public static string VeryfyMailTitle = "Mail not verified";
        public static string VeryfyMailHeader = "Mail not verified";
        public static string VeryfyMailMessage = "Your mail is not veryfied. You are not allowed to use service.";
    }
}