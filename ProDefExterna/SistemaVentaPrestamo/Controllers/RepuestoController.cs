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
    public class RepuestoController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: Repuesto
        public ActionResult Index()
        {
            var repuesto = db.Repuesto.Include(r => r.TipoRepuesto);
            return View(repuesto.ToList());
        }

        // GET: Repuesto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repuesto repuesto = db.Repuesto.Find(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        // GET: Repuesto/Create
        public ActionResult Create()
        {
            ViewBag.idTipoRepuesto = new SelectList(db.TipoRepuesto, "idTipoRepuesto", "Nombre");
            return View();
        }

        // POST: Repuesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRepuesto,Nombre,Cantidad,Precio,idTipoRepuesto")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.Repuesto.Add(repuesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoRepuesto = new SelectList(db.TipoRepuesto, "idTipoRepuesto", "Nombre", repuesto.idTipoRepuesto);
            return View(repuesto);
        }

        // GET: Repuesto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repuesto repuesto = db.Repuesto.Find(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoRepuesto = new SelectList(db.TipoRepuesto, "idTipoRepuesto", "Nombre", repuesto.idTipoRepuesto);
            return View(repuesto);
        }

        // POST: Repuesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRepuesto,Nombre,Cantidad,Precio,idTipoRepuesto")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repuesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipoRepuesto = new SelectList(db.TipoRepuesto, "idTipoRepuesto", "Nombre", repuesto.idTipoRepuesto);
            return View(repuesto);
        }

        // GET: Repuesto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repuesto repuesto = db.Repuesto.Find(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        // POST: Repuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repuesto repuesto = db.Repuesto.Find(id);
            db.Repuesto.Remove(repuesto);
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
