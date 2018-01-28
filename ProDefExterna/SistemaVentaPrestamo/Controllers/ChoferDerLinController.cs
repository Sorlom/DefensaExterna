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
    [Authorize(Roles = "R1")]
    public class ChoferDerLinController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: ChoferDerLin
        public ActionResult Index()
        {
            var choferDerLin = db.ChoferDerLin.Include(c => c.DerechoLinea).Include(c => c.Personal);
            return View(choferDerLin.ToList());
        }

        // GET: ChoferDerLin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChoferDerLin choferDerLin = db.ChoferDerLin.Find(id);
            if (choferDerLin == null)
            {
                return HttpNotFound();
            }
            return View(choferDerLin);
        }

        // GET: ChoferDerLin/Create
        public ActionResult Create()
        {
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion");
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password");
            return View();
        }

        // POST: ChoferDerLin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idChoDer,Login,idDerechoLinea,Descripcion")] ChoferDerLin choferDerLin)
        {
            if (ModelState.IsValid)
            {
                db.ChoferDerLin.Add(choferDerLin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", choferDerLin.idDerechoLinea);
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password", choferDerLin.Login);
            return View(choferDerLin);
        }

        // GET: ChoferDerLin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChoferDerLin choferDerLin = db.ChoferDerLin.Find(id);
            if (choferDerLin == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", choferDerLin.idDerechoLinea);
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password", choferDerLin.Login);
            return View(choferDerLin);
        }

        // POST: ChoferDerLin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idChoDer,Login,idDerechoLinea,Descripcion")] ChoferDerLin choferDerLin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choferDerLin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDerechoLinea = new SelectList(db.DerechoLinea, "idDerechoLinea", "Descripcion", choferDerLin.idDerechoLinea);
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password", choferDerLin.Login);
            return View(choferDerLin);
        }

        // GET: ChoferDerLin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChoferDerLin choferDerLin = db.ChoferDerLin.Find(id);
            if (choferDerLin == null)
            {
                return HttpNotFound();
            }
            return View(choferDerLin);
        }

        // POST: ChoferDerLin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChoferDerLin choferDerLin = db.ChoferDerLin.Find(id);
            db.ChoferDerLin.Remove(choferDerLin);
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
