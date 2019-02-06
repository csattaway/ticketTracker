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
    public class tblClosedTicketsController : Controller
    {
        private ticketTrackerEntities db = new ticketTrackerEntities();

        // GET: tblClosedTickets
        public ActionResult Index()
        {
            var tblClosedTickets = db.tblClosedTickets.Include(t => t.tblOpenTicket);
            return View(tblClosedTickets.ToList());
        }

        // GET: tblClosedTickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClosedTicket tblClosedTicket = db.tblClosedTickets.Find(id);
            if (tblClosedTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblClosedTicket);
        }

        // GET: tblClosedTickets/Create
        public ActionResult Create()
        {
            ViewBag.idsTicket = new SelectList(db.tblOpenTickets, "idsTicket", "txtUserName");
            return View();
        }

        // POST: tblClosedTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsClosedTicket,idsTicket,dtmResolved,txtUserName,blnResolved,txtResolution")] tblClosedTicket tblClosedTicket)
        {
            if (ModelState.IsValid)
            {
                db.tblClosedTickets.Add(tblClosedTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idsTicket = new SelectList(db.tblOpenTickets, "idsTicket", "txtUserName", tblClosedTicket.idsTicket);
            return View(tblClosedTicket);
        }

        // GET: tblClosedTickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClosedTicket tblClosedTicket = db.tblClosedTickets.Find(id);
            if (tblClosedTicket == null)
            {
                return HttpNotFound();
            }
            ViewBag.idsTicket = new SelectList(db.tblOpenTickets, "idsTicket", "txtUserName", tblClosedTicket.idsTicket);
            return View(tblClosedTicket);
        }

        // POST: tblClosedTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsClosedTicket,idsTicket,dtmResolved,txtUserName,blnResolved,txtResolution")] tblClosedTicket tblClosedTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblClosedTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idsTicket = new SelectList(db.tblOpenTickets, "idsTicket", "txtUserName", tblClosedTicket.idsTicket);
            return View(tblClosedTicket);
        }

        // GET: tblClosedTickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClosedTicket tblClosedTicket = db.tblClosedTickets.Find(id);
            if (tblClosedTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblClosedTicket);
        }

        // POST: tblClosedTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblClosedTicket tblClosedTicket = db.tblClosedTickets.Find(id);
            db.tblClosedTickets.Remove(tblClosedTicket);
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
