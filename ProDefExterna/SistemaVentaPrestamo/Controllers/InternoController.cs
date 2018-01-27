using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaVentaPrestamo.Models;

namespace SistemaVentaPrestamo.Controllers
{
    public class InternoController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: Interno
        public ActionResult Index()
        {
            var interno = db.Interno.Include(i => i.DerechoLinea);
            return View(interno.ToList());
        }

        // GET: Interno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interno interno = db.Interno.Find(id);
            if (interno == null)
            {
                return HttpNotFound();
            }
            return View(interno);
        }

        // GET: Interno/Create
        public ActionResult Create()
        {
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion");
            return View();
        }

        // POST: Interno/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idInterno,Chasis,Placa,Modelo,idDerechoLinea")] Interno interno)
        {
            if (ModelState.IsValid)
            {
                db.Interno.Add(interno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", interno.idDerechoLinea);
            return View(interno);
        }

        // GET: Interno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interno interno = db.Interno.Find(id);
            if (interno == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", interno.idDerechoLinea);
            return View(interno);
        }

        // POST: Interno/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idInterno,Chasis,Placa,Modelo,idDerechoLinea")] Interno interno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", interno.idDerechoLinea);
            return View(interno);
        }

        // GET: Interno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interno interno = db.Interno.Find(id);
            if (interno == null)
            {
                return HttpNotFound();
            }
            return View(interno);
        }

        // POST: Interno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interno interno = db.Interno.Find(id);
            db.Interno.Remove(interno);
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
