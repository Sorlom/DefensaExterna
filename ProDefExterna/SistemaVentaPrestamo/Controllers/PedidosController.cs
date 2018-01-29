using SistemaVentaPrestamo.Models;
using SistemaVentaPrestamo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentaPrestamo.Controllers
{
    [Authorize(Roles = "R1,R2")]
    public class PedidosController : Controller
    {
        BDDEFEXTEntities db = new BDDEFEXTEntities();
        // GET: Pedidos

        public ActionResult NuevoPedido()
        {
            var pedidoVista = new VistaPedido();
            pedidoVista.DerechoLinea = new DerechoLinea();
            pedidoVista.Encargado = new Personal();
            pedidoVista.Chofer = new Personal();
            pedidoVista.Repuestos = new List<OrdenRepuesto>();

            Session["pedidoVista"] = pedidoVista;

            var list = db.DerechoLinea.ToList();
            list.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
            list = list.OrderBy(c => c.idDueño).ToList();
            ViewBag.idDerechoLinea = new SelectList(list, "idDerechoLinea", "idDueño");

            var list1 = db.Personal.ToList();
            list1.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
            list1 = list1.OrderBy(c => c.Login).ToList();
            ViewBag.idChofer = new SelectList(list1, "Login", "nombreCompleto");

            var list2 = db.Personal.ToList();
            list2.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
            list2 = list2.OrderBy(c => c.Login).ToList();
            ViewBag.idEncargado = new SelectList(list2, "Login", "nombreCompleto");


            return View(pedidoVista);
        }
        [HttpPost]
        public ActionResult NuevoPedido(VistaPedido pedidoVista)
        {
            pedidoVista = Session["pedidoVista"] as VistaPedido;

            var idDerechoLinea = int.Parse(Request["idDerechoLinea"]);
            var idChofer = Request["idChofer"].ToString();
            var idEncargado = Request["idEncargado"].ToString();

            if (idDerechoLinea == 0|| idChofer == "" || idEncargado == "")
            {
                var lista = db.DerechoLinea.ToList();
                lista.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
                lista = lista.OrderBy(c => c.idDueño).ToList();
                ViewBag.idDerechoLinea = new SelectList(lista, "idDerechoLinea", "idDueño");
    

                var listb = db.Personal.ToList();
                listb.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
                listb = listb.OrderBy(c => c.Login).ToList();
                ViewBag.idChofer = new SelectList(listb, "Login", "nombreCompleto");


                var listc = db.Personal.ToList();
                listc.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
                listc = listc.OrderBy(c => c.Login).ToList();
                ViewBag.idEncargado = new SelectList(listc, "Login", "nombreCompleto");
                ViewBag.Error = "Debe Elegir un Encargado, Chofer y Derecho de Linea";

                return View(pedidoVista);
            }
            if (pedidoVista.Repuestos.Count == 0)
            {
                var lista = db.DerechoLinea.ToList();
                lista.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
                lista = lista.OrderBy(c => c.idDueño).ToList();
                ViewBag.idDerechoLinea = new SelectList(lista, "idDerechoLinea", "idDueño");

                var listb = db.Personal.ToList();
                listb.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
                listb = listb.OrderBy(c => c.Login).ToList();
                ViewBag.idChofer = new SelectList(listb, "Login", "nombreCompleto");

                var listc = db.Personal.ToList();
                listc.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
                listc = listc.OrderBy(c => c.Login).ToList();
                ViewBag.idEncargado = new SelectList(listc, "Login", "nombreCompleto");
                ViewBag.Error = "Deben Elegir un Producto";

                return View(pedidoVista);
            }
            int prestamoID = 0;
            int cantRepuesto = 0;
            //Prueba de Transaccion
            int i = 0;

            using (var transaction = db.Database.BeginTransaction())
            //using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    DateTime fecLim = DateTime.Now.AddDays(3);
                    var prestamo = new Prestamo
                    {
                        idDerechoLinea = idDerechoLinea,
                        fechaInicio = DateTime.Now,
                        fechaLimite = fecLim,
                        idChofer = idChofer.ToString(),
                        idEncargado = idEncargado.ToString(),
                        Descripcion = Request["Descripcion"].ToString()
                    };
                    db.Prestamo.Add(prestamo);
                    db.SaveChanges();

                    prestamoID = db.Prestamo.ToList().Select(o => o.idPrestamo).Max();

                    foreach (var item in pedidoVista.Repuestos)
                    {
                        var detallePedido = new DetallePrestamo
                        {
                            idPrestamo = prestamoID,
                            idRepuesto = item.idRepuesto,
                            Cantidad = int.Parse(item.MontoCantidad.ToString()),
                            Estado = "Prestado"
                        };
                        cantRepuesto = db.Repuesto.Where(r => r.idRepuesto == detallePedido.idRepuesto).FirstOrDefault().Cantidad;
                        if (cantRepuesto - detallePedido.Cantidad <= 0)
                        {
                            transaction.Rollback();
                            ViewBag.Error = "Error: El repuesto pedido excede la cantidad de Inventario";

                            var listt1 = db.DerechoLinea.ToList();
                            listt1.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
                            listt1 = listt1.OrderBy(c => c.idDueño).ToList();
                            ViewBag.idDerechoLinea = new SelectList(listt1, "idDerechoLinea", "idDueño");

                            var listt2 = db.Personal.ToList();
                            listt2.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
                            listt2 = listt2.OrderBy(c => c.Login).ToList();
                            ViewBag.idChofer = new SelectList(listt2, "Login", "nombreCompleto");

                            var listt3 = db.Personal.ToList();
                            listt3.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
                            listt3 = listt3.OrderBy(c => c.Login).ToList();
                            ViewBag.idEncargado = new SelectList(listt3, "Login", "nombreCompleto");

                            pedidoVista = new VistaPedido();
                            pedidoVista.DerechoLinea = new DerechoLinea();
                            pedidoVista.Encargado = new Personal();
                            pedidoVista.Chofer = new Personal();
                            pedidoVista.Repuestos = new List<OrdenRepuesto>();
                            Session["pedidoVista"] = pedidoVista;

                            return View(pedidoVista);
                        }
                        else
                        {

                            db.DetallePrestamo.Add(detallePedido);
                            db.SaveChanges();

                            //Prueba de Transaccion
                            //i++;
                            //if (i > 2)
                            //{
                            //  int a = 0;
                            //    i /= a;
                            //}
                        }
                    }
                    transaction.Commit();
                    //scope.Complete();
                    
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error: " + ex.Message;
                    transaction.Rollback();
                    

                    var listt1 = db.DerechoLinea.ToList();
                    listt1.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
                    listt1 = listt1.OrderBy(c => c.idDueño).ToList();
                    ViewBag.idDerechoLinea = new SelectList(listt1, "idDerechoLinea", "idDueño");

                    var listt2 = db.Personal.ToList();
                    listt2.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
                    listt2 = listt2.OrderBy(c => c.Login).ToList();
                    ViewBag.idChofer = new SelectList(listt2, "Login", "nombreCompleto");

                    var listt3 = db.Personal.ToList();
                    listt3.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
                    listt3 = listt3.OrderBy(c => c.Login).ToList();
                    ViewBag.idEncargado = new SelectList(listt3, "Login", "nombreCompleto");


                    return View(pedidoVista);
                }
              
            }

            ViewBag.Message = string.Format("La orden: {0}, Grabada Exitosamente", prestamoID);

            var list = db.DerechoLinea.ToList();
            list.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
            list = list.OrderBy(c => c.idDueño).ToList();
            ViewBag.idDerechoLinea = new SelectList(list, "idDerechoLinea", "idDueño");

            var list1 = db.Personal.ToList();
            list1.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
            list1 = list1.OrderBy(c => c.Login).ToList();
            ViewBag.idChofer = new SelectList(list1, "Login", "nombreCompleto");

            var list2 = db.Personal.ToList();
            list2.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
            list2 = list2.OrderBy(c => c.Login).ToList();
            ViewBag.idEncargado = new SelectList(list2, "Login", "nombreCompleto");

            pedidoVista = new VistaPedido();
            pedidoVista.DerechoLinea = new DerechoLinea();
            pedidoVista.Encargado = new Personal();
            pedidoVista.Chofer = new Personal();
            pedidoVista.Repuestos = new List<OrdenRepuesto>();
            Session["pedidoVista"] = pedidoVista;

            return View(pedidoVista);
        }

        public ActionResult AddRepuesto()
        {
            var list = db.Repuesto.ToList();
            list.Add(new OrdenRepuesto { idRepuesto = 0, Nombre = "[seleccione un Repuesto...]" });
            list = list.OrderBy(r => r.Nombre).ToList();
            ViewBag.idRepuesto = new SelectList(list, "idRepuesto", "Nombre");

            return View();
        }
        [HttpPost]
        public ActionResult AddRepuesto(OrdenRepuesto ordenRepuesto)
        {
            var pedidoVista = Session["pedidoVista"] as VistaPedido;

            var idRepuesto = int.Parse(Request["idRepuesto"]);
            if (idRepuesto == 0)
            {
                var list = db.Repuesto.ToList();
                list.Add(new OrdenRepuesto { idRepuesto = 0, Nombre = "[seleccione un Repuesto...]" });
                list = list.OrderBy(r => r.Nombre).ToList();
                ViewBag.idRepuesto = new SelectList(list, "idRepuesto", "Nombre");
                ViewBag.Error = "Debe seleccionar un Repuesto";

                return View(ordenRepuesto);
            }
            var repuesto = db.Repuesto.Find(idRepuesto);
            if (repuesto == null)
            {
                var list = db.Repuesto.ToList();
                list.Add(new OrdenRepuesto { idRepuesto = 0, Nombre = "[seleccione un Repuesto...]" });
                list = list.OrderBy(r => r.Nombre).ToList();
                ViewBag.idRepuesto = new SelectList(list, "idRepuesto", "Nombre");
                ViewBag.Error = "Repuesto no existe";

                return View(ordenRepuesto);
            }

            ordenRepuesto = pedidoVista.Repuestos.Find(p => p.idRepuesto == idRepuesto);
            if (ordenRepuesto == null)
            {

                ordenRepuesto = new OrdenRepuesto
                {
                    Nombre = repuesto.Nombre,
                    idRepuesto = repuesto.idRepuesto,
                    Precio = repuesto.Precio,
                    MontoCantidad = float.Parse(Request["MontoCantidad"])

                };
                pedidoVista.Repuestos.Add(ordenRepuesto);
                
            }
            else
            {
                ordenRepuesto.MontoCantidad += float.Parse(Request["MontoCantidad"]);
            }
            

            var lista = db.DerechoLinea.ToList();
            lista.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
            lista = lista.OrderBy(c => c.idDueño).ToList();
            ViewBag.idDerechoLinea = new SelectList(lista, "idDerechoLinea", "idDueño");

            var list1 = db.Personal.ToList();
            list1.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
            list1 = list1.OrderBy(c => c.Login).ToList();
            ViewBag.idChofer = new SelectList(list1, "Login", "nombreCompleto");

            var list2 = db.Personal.ToList();
            list2.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
            list2 = list2.OrderBy(c => c.Login).ToList();
            ViewBag.idEncargado = new SelectList(list2, "Login", "nombreCompleto");
            return View("NuevoPedido",pedidoVista);

        }
        public ActionResult RemoveRepuesto(int id)
        {
            var pedidoVista = Session["pedidoVista"] as VistaPedido;           
            OrdenRepuesto ordenRepuesto = pedidoVista.Repuestos.Find(p => p.idRepuesto == id);
            pedidoVista.Repuestos.Remove(ordenRepuesto);

            var lista = db.DerechoLinea.ToList();
            lista.Add(new DerechoLinea { idDerechoLinea = 0, idDueño = "[seleccione un derecho de linea...]" });
            lista = lista.OrderBy(c => c.idDueño).ToList();
            ViewBag.idDerechoLinea = new SelectList(lista, "idDerechoLinea", "idDueño");

            var list1 = db.Personal.ToList();
            list1.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Chofer...]" });
            list1 = list1.OrderBy(c => c.Login).ToList();
            ViewBag.idChofer = new SelectList(list1, "Login", "nombreCompleto");

            var list2 = db.Personal.ToList();
            list2.Add(new Personal { Login = "", nombreCompleto = "[seleccione un Encargado...]" });
            list2 = list2.OrderBy(c => c.Login).ToList();
            ViewBag.idEncargado = new SelectList(list2, "Login", "nombreCompleto");

            return View("NuevoPedido", pedidoVista);

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