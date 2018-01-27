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
    public class TipoRepuestoController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: TipoRepuesto
        public ActionResult Index()
        {
            return View(db.TipoRepuesto.ToList());
        }

        // GET: TipoRepuesto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRepuesto tipoRepuesto = db.TipoRepuesto.Find(id);
            if (tipoRepuesto == null)
            {
                return HttpNotFound();
            }
            return View(tipoRepuesto);
        }

        // GET: TipoRepuesto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRepuesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoRepuesto,Nombre,Descripcion")] TipoRepuesto tipoRepuesto)
        {
            if (ModelState.IsValid)
            {
                db.TipoRepuesto.Add(tipoRepuesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoRepuesto);
        }

        // GET: TipoRepuesto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRepuesto tipoRepuesto = db.TipoRepuesto.Find(id);
            if (tipoRepuesto == null)
            {
                return HttpNotFound();
            }
            return View(tipoRepuesto);
        }

        // POST: TipoRepuesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoRepuesto,Nombre,Descripcion")] TipoRepuesto tipoRepuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoRepuesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoRepuesto);
        }

        // GET: TipoRepuesto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRepuesto tipoRepuesto = db.TipoRepuesto.Find(id);
            if (tipoRepuesto == null)
            {
                return HttpNotFound();
            }
            return View(tipoRepuesto);
        }

        // POST: TipoRepuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoRepuesto tipoRepuesto = db.TipoRepuesto.Find(id);
            db.TipoRepuesto.Remove(tipoRepuesto);
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
