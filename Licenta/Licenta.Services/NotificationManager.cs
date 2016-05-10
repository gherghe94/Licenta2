using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class NotificationManager
    {
        #region Announcement Notification

        public static void SendNotification(Announcement announcement, string email)
        {
            try
            {
                IEmailContent content = new NotificationEmailContent(GetContent(announcement), GetSubject(announcement));
                EmailSender eSender = new EmailSender(email, content);
                eSender.Send();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static string GetContent(Announcement announcement)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("<h3> Notification from : {0} </h3> <br>", announcement.Teacher.FullName));
            stringBuilder.Append(string.Format("<b> Info: </b> <br> {0}", announcement.Description));
            stringBuilder.Append(GetFooter());
            return stringBuilder.ToString();
        }

        private static string GetFooter()
        {
            return "<br> <br> <i> For more information, please visit orar.unitbv.ro and log in to see the announcements. </i>";
        }

        private static string GetGroupName(Group group)
        {
            return string.Format("{0} {1}", group.Specialization, group.Name);
        }

        private static string GetSubject(Announcement announcement)
        {
            return string.Format("[Orar] - Unit Bv - Notification for {0}", GetGroupName(announcement.Group));
        }

        #endregion Announcement Notification
    }
}
