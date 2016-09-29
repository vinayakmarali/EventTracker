using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventTracker.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            var db = new EventsDBEntities();
            var events = db.Events.ToList();
            return Json(events, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddEvent(string newEvent)
        {
            var db = new EventsDBEntities();
            if (! (db.Events.Any(o => o.EventName == newEvent)))
            { 
                db.Events.Add(new Event() { EventName = newEvent });
                db.SaveChanges();
            }
            var events = db.Events.ToList();
            return Json(events, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEvent(Event delEvent)
        {
            var db = new EventsDBEntities();
            var events = db.Events.Find(delEvent.Id);
            db.Events.Remove(events);
            db.Attendees.RemoveRange(db.Attendees.Where(m => m.EventId == delEvent.Id));
            db.SaveChanges();
            var eventsList = db.Events.ToList();
            return Json(eventsList, JsonRequestBehavior.AllowGet);
        }
    }
}