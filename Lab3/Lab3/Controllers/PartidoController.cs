using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Data;
using Lab3.DBContext;
using Newtonsoft.Json;
using TDA;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class PartidoController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
       
        // GET: Partido
        public ActionResult Index()
        {
            return View(db.Arbolito.ToList());
        }

        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NoPartido,FechaPartido,Grupo,Pais1,Pais2,Estadio")] Partido Partido)
        {
            try
            {
                // TODO: Add insert logic here
                db.Arbolito.Insertar(Partido);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partido/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
