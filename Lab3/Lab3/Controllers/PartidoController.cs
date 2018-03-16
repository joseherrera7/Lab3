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
                        JsonReader<ArbolAVLBase<Partido, int>> reader = new JsonReader<TDA.ArbolAVLBase<Partido, int>>();
                        db.Arbolito = reader.Data(stream);
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
            public ArbolAVLBase<Partido, int> Data(Stream rutaOrigen)
            {
                try
                {
                    ArbolAVLBase<Partido, int> data;
                    StreamReader reader = new StreamReader(rutaOrigen);
                    string temp = reader.ReadToEnd();
                    data = JsonConvert.DeserializeObject<ArbolAVLBase<Partido, int>>(temp);
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
