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
    public class PrestamosController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamo = db.Prestamo.Include(p => p.DerechoLinea).Include(p => p.Personal).Include(p => p.Personal1);
            return View(prestamo.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "idDerechoLinea");
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Login");
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Login");
            return View();

        }
        // POST: Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPrestamo,Descripcion,fechaInicio,fechaLimite,idDerechoLinea,idChofer,idEncargado")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Prestamo.Add(prestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", prestamo.idDerechoLinea);
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password", prestamo.idChofer);
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password", prestamo.idEncargado);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", prestamo.idDerechoLinea);
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password", prestamo.idChofer);
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password", prestamo.idEncargado);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPrestamo,Descripcion,fechaInicio,fechaLimite,idDerechoLinea,idChofer,idEncargado")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", prestamo.idDerechoLinea);
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password", prestamo.idChofer);
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password", prestamo.idEncargado);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
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
