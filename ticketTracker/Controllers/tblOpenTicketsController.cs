using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ticketTracker.Models;

namespace ticketTracker.Controllers
{
    public class tblOpenTicketsController : Controller
    {
        private ticketTrackerEntities db = new ticketTrackerEntities();

        // GET: tblOpenTickets
        public ActionResult Index()
        {
            
            return View(db.tblOpenTickets.ToList());
        }

        // GET: tblOpenTickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOpenTicket tblOpenTicket = db.tblOpenTickets.Find(id);
            if (tblOpenTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblOpenTicket);
        }

        // GET: tblOpenTickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblOpenTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsTicket,txtUserName,dtmCreated,dtmResolved,blnResolved,txtIssue")] tblOpenTicket tblOpenTicket)
        {
            if (ModelState.IsValid)
            {
                db.tblOpenTickets.Add(tblOpenTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblOpenTicket);
        }

        // GET: tblOpenTickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOpenTicket tblOpenTicket = db.tblOpenTickets.Find(id);
            if (tblOpenTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblOpenTicket);
        }

        // POST: tblOpenTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsTicket,txtUserName,dtmCreated,dtmResolved,blnResolved,txtIssue")] tblOpenTicket tblOpenTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblOpenTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblOpenTicket);
        }

        // GET: tblOpenTickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOpenTicket tblOpenTicket = db.tblOpenTickets.Find(id);
            if (tblOpenTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblOpenTicket);
        }

        // POST: tblOpenTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblOpenTicket tblOpenTicket = db.tblOpenTickets.Find(id);
            db.tblOpenTickets.Remove(tblOpenTicket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
