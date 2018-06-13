using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using System.Web;
using AILibrary.Models;
using Microsoft.AspNet.Identity;

namespace AILibrary.Controllers.Helpers
{
    public class DeleteAccountExecutor
    {
        public ApplicationUser UserToDelete { get; set; }
        public ApplicationUserManager Manager { get; set; }
        public HttpContextBase context;

        private long? Interval;
        private Timer Timer;

        public void DelegateDelete()
        {
            if (UserToDelete == null)
            {
                throw new Exception("User to delete is not chosen!");
            }

            if (Manager == null)
            {
                throw new Exception("ApplicationUserManager is not set!");
            }

            if (!Interval.HasValue)
            {
                throw new Exception("Interval is not set!");
            }

            Timer = new Timer();
            Timer.Interval = Interval.Value;
            Timer.Elapsed += DeleteUser;
            Timer.AutoReset = false;
            Timer.Start();
        }

        public void SetIntervalUsingMiliseconds(long interval)
        {
            Interval = interval;
        }

        public void SetIntervalUsingSeconds(long interval)
        {
            Interval = interval * 1000;
        }

        public void SetIntervalUsingMinutes(long interval)
        {
            Interval = interval * 1000 * 60;
        }

        public void SetIntervalUsingHours(long interval)
        {
            Interval = interval * 1000 * 60 * 60;
        }

        private void DeleteUser(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();

            var updatedUserToDelete = new ApplicationDbContext().Users.FirstOrDefault(user => user.Id.Equals(UserToDelete.Id) && !user.EmailConfirmed);

            if (updatedUserToDelete != null)
            {
                Manager.Delete(updatedUserToDelete);
            }
        }
    }
}