using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using multas.Models;

namespace multas.Controllers
{
    public class AgentesController : Controller
    {
        private MultasDb db = new MultasDb();

        // GET: Agentes
        public ActionResult Index()
        {
            var listaAgentes = db.Agentes.ToList().OrderBy(a=>a.Nome);
            return View(listaAgentes);
        }

        // GET: Agentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                return RedirectToAction("Index");
            }
            return View(agente);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente, HttpPostedFileBase fileUploadFotografia)
        {
            int novoID = 0;

            if (db.Agentes.Count() == 0){
                novoID = 1;
            }else {
                novoID = db.Agentes.Max(a => a.ID) + 1;
            }

            agente.ID = novoID;

            string nomeFotografia = "Agente_" + novoID + ".jpg";
            string caminhoParaFotografia = Path.Combine(Server.MapPath("~/imagens/"), nomeFotografia);

            if(fileUploadFotografia != null) {
                agente.Fotografia = nomeFotografia;
            }else{
                ModelState.AddModelError("", "Nao foi fornecida uma imagem...");
                return View(agente);
            }

            if (ModelState.IsValid)
            {
                try{
                    db.Agentes.Add(agente);
                    db.SaveChanges();
                    fileUploadFotografia.SaveAs(caminhoParaFotografia);
                    return RedirectToAction("Index");
                }
                catch(Exception){
                    ModelState.AddModelError("", "Ocorreu um erro nao determinado na criacao do novo agente");
                }
                
            }

            return View(agente);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                return RedirectToAction("Index");
            }
            return View(agente);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agente);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agentes agente = db.Agentes.Find(id);
            try {
                
                db.Agentes.Remove(agente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception){
                ModelState.AddModelError("", string.Format("Nao foi possivel remover o Agente '{0}', porque existem {1} multas associadas a ele.", agente.Nome, agente.ListaDeMultas.Count));
            }
            return View(agente);
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
