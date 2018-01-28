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
    [Authorize(Roles = "R1,R2")]
    public class DevolucionController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: Devolucion
        public ActionResult Index()
        {
            var devolucion = db.Devolucion.Include(d => d.Prestamo);
            return View(devolucion.ToList());
        }

        // GET: Devolucion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // GET: Devolucion/Create
        public ActionResult Create()
        {
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion");
            return View();
        }

        // POST: Devolucion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDevolucion,Descripcion,Fecha,idPrestamo")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Devolucion.Add(devolucion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion", devolucion.idPrestamo);
            return View(devolucion);
        }

        // GET: Devolucion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion", devolucion.idPrestamo);
            return View(devolucion);
        }

        // POST: Devolucion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDevolucion,Descripcion,Fecha,idPrestamo")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devolucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "idPrestamo", "Descripcion", devolucion.idPrestamo);
            return View(devolucion);
        }

        // GET: Devolucion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // POST: Devolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devolucion devolucion = db.Devolucion.Find(id);
            db.Devolucion.Remove(devolucion);
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
