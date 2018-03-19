using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Data;
using Lab3.DBContext;
using TDA;
using Lab3.Models;
using Newtonsoft.Json;

namespace Lab3.Controllers
{
    public class PartidoController<T> : Controller
    {
        DefaultConnection<T> db = DefaultConnection<T>.getInstance;

        // GET: Partido
        [HttpPost]
        
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
        public ActionResult Edit([Bind(Include = "NoPartido,FechaPartido,Grupo,Pais1,Pais2,Estadio")]Partido Partido)
        {
            try
            {
                Partido PartidoBuscado = db.Arbolito.Buscar(Partido.);
                if (PartidoBuscado == null)
                {
                    return HttpNotFound();
                }
                PartidoBuscado.Estadio = Partido.Estadio;
                PartidoBuscado.Grupo = Partido.Grupo;
                PartidoBuscado.FechaPartido = PartidoBuscado.FechaPartido;
                PartidoBuscado.NoPartido = PartidoBuscado.NoPartido;
                PartidoBuscado.Pais1 = Partido.Pais1;
                PartidoBuscado.Pais2 = Partido.Pais2;

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Partido/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Partido PartidoBuscado = db.Arbolito.Buscar(id);

            if (PartidoBuscado == null)
            {
                return HttpNotFound();
            }

            return View(PartidoBuscado);
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(T id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Partido PartidoBuscado = db.Arbolito.Buscar(id);

                if (PartidoBuscado == null)
                {
                    return HttpNotFound();
                }

                return View(PartidoBuscado);
                
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// GET UPLOAD
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            return View();
        }
        /// <summary>
        /// Se sube un archivo
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string filePath = string.Empty;
                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".json"))
                    {
                        Stream stream = upload.InputStream;
                        JsonReader<Partido[]> reader = new JsonReader<Partido[]>();
                        
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();

        }
        public class JsonReader<T>
        {
            /// <summary>
            /// Lector de Archivos tipo Json
            /// </summary>
            /// <param name="rutaOrigen">Ruta de archivos</param>
            /// <returns></returns>
            public Partido[] Data(Stream rutaOrigen)
            {
                try
                {
                    Partido[] data;
                    StreamReader reader = new StreamReader(rutaOrigen);
                    string temp = reader.ReadToEnd();
                    data = JsonConvert.DeserializeObject<Partido[]>(temp);
                    reader.Close();
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }
}
