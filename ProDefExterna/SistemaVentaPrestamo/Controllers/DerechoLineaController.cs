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
    public class DerechoLineaController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: DerechoLinea
        public ActionResult Index()
        {
            var derechoLinea = db.DerechoLinea.Include(d => d.Personal);
            return View(derechoLinea.ToList());
        }

        // GET: DerechoLinea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DerechoLinea derechoLinea = db.DerechoLinea.Find(id);
            if (derechoLinea == null)
            {
                return HttpNotFound();
            }
            return View(derechoLinea);
        }

        // GET: DerechoLinea/Create
        public ActionResult Create()
        {
            ViewBag.idDueño = new SelectList(db.Personal, "Login", "Password");
            return View();
        }

        // POST: DerechoLinea/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDerechoLinea,Descripcion,Estado,idDueño")] DerechoLinea derechoLinea)
        {
            if (ModelState.IsValid)
            {
                db.DerechoLinea.Add(derechoLinea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDueño = new SelectList(db.Personal, "Login", "Password", derechoLinea.idDueño);
            return View(derechoLinea);
        }

        // GET: DerechoLinea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DerechoLinea derechoLinea = db.DerechoLinea.Find(id);
            if (derechoLinea == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDueño = new SelectList(db.Personal, "Login", "Password", derechoLinea.idDueño);
            return View(derechoLinea);
        }

        // POST: DerechoLinea/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDerechoLinea,Descripcion,Estado,idDueño")] DerechoLinea derechoLinea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(derechoLinea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDueño = new SelectList(db.Personal, "Login", "Password", derechoLinea.idDueño);
            return View(derechoLinea);
        }

        // GET: DerechoLinea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DerechoLinea derechoLinea = db.DerechoLinea.Find(id);
            if (derechoLinea == null)
            {
                return HttpNotFound();
            }
            return View(derechoLinea);
        }

        // POST: DerechoLinea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DerechoLinea derechoLinea = db.DerechoLinea.Find(id);
            db.DerechoLinea.Remove(derechoLinea);
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
