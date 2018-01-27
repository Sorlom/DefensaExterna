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
    public class RolesPersonalController : Controller
    {
        private BDDEFEXTEntities db = new BDDEFEXTEntities();

        // GET: RolesPersonal
        public ActionResult Index()
        {
            var rolesPersonal = db.RolesPersonal.Include(r => r.Personal).Include(r => r.Roles);
            return View(rolesPersonal.ToList());
        }

        // GET: RolesPersonal/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesPersonal rolesPersonal = db.RolesPersonal.Find(id);
            if (rolesPersonal == null)
            {
                return HttpNotFound();
            }
            return View(rolesPersonal);
        }

        // GET: RolesPersonal/Create
        public ActionResult Create()
        {
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password");
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre");
            return View();
        }

        // POST: RolesPersonal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,idRol,Descripcion")] RolesPersonal rolesPersonal)
        {
            if (ModelState.IsValid)
            {
                db.RolesPersonal.Add(rolesPersonal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Login = new SelectList(db.Personal, "Login", "Password", rolesPersonal.Login);
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", rolesPersonal.idRol);
            return View(rolesPersonal);
        }

        // GET: RolesPersonal/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesPersonal rolesPersonal = db.RolesPersonal.Find(id);
            if (rolesPersonal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password", rolesPersonal.Login);
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", rolesPersonal.idRol);
            return View(rolesPersonal);
        }

        // POST: RolesPersonal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Login,idRol,Descripcion")] RolesPersonal rolesPersonal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolesPersonal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Login = new SelectList(db.Personal, "Login", "Password", rolesPersonal.Login);
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", rolesPersonal.idRol);
            return View(rolesPersonal);
        }

        // GET: RolesPersonal/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesPersonal rolesPersonal = db.RolesPersonal.Find(id);
            if (rolesPersonal == null)
            {
                return HttpNotFound();
            }
            return View(rolesPersonal);
        }

        // POST: RolesPersonal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RolesPersonal rolesPersonal = db.RolesPersonal.Find(id);
            db.RolesPersonal.Remove(rolesPersonal);
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
