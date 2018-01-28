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
    public class DetallePrestamoController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: DetallePrestamo
        public ActionResult Index()
        {
            var detallePrestamo = db.DetallePrestamo.Include(d => d.Prestamo).Include(d => d.Repuesto);
            return View(detallePrestamo.ToList());
        }

        // GET: DetallePrestamo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePrestamo detallePrestamo = db.DetallePrestamo.Find(id);
            if (detallePrestamo == null)
            {
                return HttpNotFound();
            }
            return View(detallePrestamo);
        }

        // GET: DetallePrestamo/Create
        public ActionResult Create()
        {
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion");
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "Nombre");
            return View();
        }

        // POST: DetallePrestamo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetPre,idPrestamo,idRepuesto,Cantidad,Estado")] DetallePrestamo detallePrestamo)
        {
            if (ModelState.IsValid)
            {
                db.DetallePrestamo.Add(detallePrestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion", detallePrestamo.idPrestamo);
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "Nombre", detallePrestamo.idRepuesto);
            return View(detallePrestamo);
        }

        // GET: DetallePrestamo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePrestamo detallePrestamo = db.DetallePrestamo.Find(id);
            if (detallePrestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion", detallePrestamo.idPrestamo);
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "Nombre", detallePrestamo.idRepuesto);
            return View(detallePrestamo);
        }

        // POST: DetallePrestamo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetPre,idPrestamo,idRepuesto,Cantidad,Estado")] DetallePrestamo detallePrestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePrestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion", detallePrestamo.idPrestamo);
            ViewBag.idRepuesto = new SelectList(db.Repuesto, "idRepuesto", "Nombre", detallePrestamo.idRepuesto);
            return View(detallePrestamo);
        }

        // GET: DetallePrestamo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePrestamo detallePrestamo = db.DetallePrestamo.Find(id);
            if (detallePrestamo == null)
            {
                return HttpNotFound();
            }
            return View(detallePrestamo);
        }

        // POST: DetallePrestamo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePrestamo detallePrestamo = db.DetallePrestamo.Find(id);
            db.DetallePrestamo.Remove(detallePrestamo);
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
