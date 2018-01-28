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
    [Authorize(Roles = "R1,R3")]
    public class VentaController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: Venta
        public ActionResult Index()
        {
            var venta = db.Venta.Include(v => v.DerechoLinea).Include(v => v.Personal).Include(v => v.Personal1);
            return View(venta.ToList());
        }

        // GET: Venta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion");
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password");
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password");
            return View();
        }

        // POST: Venta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVenta,Descripcion,fechaInicio,fechaLimite,Monto,idDerechoLinea,idChofer,idEncargado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", venta.idDerechoLinea);
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password", venta.idChofer);
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password", venta.idEncargado);
            return View(venta);
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", venta.idDerechoLinea);
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password", venta.idChofer);
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password", venta.idEncargado);
            return View(venta);
        }

        // POST: Venta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVenta,Descripcion,fechaInicio,fechaLimite,Monto,idDerechoLinea,idChofer,idEncargado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", venta.idDerechoLinea);
            ViewBag.idChofer = new SelectList(db.Personal, "Login", "Password", venta.idChofer);
            ViewBag.idEncargado = new SelectList(db.Personal, "Login", "Password", venta.idEncargado);
            return View(venta);
        }

        // GET: Venta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Venta.Find(id);
            db.Venta.Remove(venta);
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
