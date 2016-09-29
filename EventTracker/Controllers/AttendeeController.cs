using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventTracker.Controllers
{
    public class AttendeeController : Controller
    {
        // GET: Attendee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAttendees(Event events)
        {
            var db = new EventsDBEntities();
            int eId = db.Events.Find(events.Id).Id;
            var attendees = from t in db.Attendees
                         where t.EventId == eId
                         select t;
            return Json(attendees, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddAttendee(string firstName, string lastName, Event events)
        {
            var db = new EventsDBEntities();
            var eId = db.Events.Find(events.Id);
            if (!(db.Attendees.Any(o => o.FirstName == firstName && o.LastName == lastName && o.EventId == events.Id)))
            {
                db.Attendees.Add(new Attendee() { FirstName = firstName, LastName = lastName, EventId = eId.Id });
                db.SaveChanges();
            }
            var attendees = from t in db.Attendees
                            where t.EventId == eId.Id
                            select t;
            return Json(attendees, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAttendee(Attendee delAttendee, Event events)
        {
            var db = new EventsDBEntities();
            int eId = db.Events.Find(events.Id).Id;
            var attendee = db.Attendees.Find(delAttendee.AttendeeId);
            db.Attendees.Remove(attendee);
            db.SaveChanges();
            var attendees = from t in db.Attendees
                            where t.EventId == eId
                            select t;
            return Json(attendees, JsonRequestBehavior.AllowGet);
        }

    }
}