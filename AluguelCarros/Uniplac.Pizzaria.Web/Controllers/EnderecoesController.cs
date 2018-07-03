using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AluguelCarros.Dominio.Features.Enderecos;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Enderecos;

namespace Uniplac.Pizzaria.Web.Controllers
{
    public class EnderecoesController : Controller
    {
        private EnderecoRepositorio db = new EnderecoRepositorio();

        // GET: Enderecoes
        public ActionResult Index()
        {
            return View(db.BuscarTudo());
        }

        // GET: Enderecoes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.BuscarPor((long)id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // GET: Enderecoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enderecoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rua,Bairro,Cidade,Cep,Estado,Numero")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Adicionar(endereco);
                return RedirectToAction("Index");
            }

            return View(endereco);
        }

        // GET: Enderecoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.BuscarPor((long)id);
            db.Editar(endereco);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // POST: Enderecoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rua,Bairro,Cidade,Cep,Estado,Numero")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Editar(endereco);
                return RedirectToAction("Index");
            }
            return View(endereco);
        }

        // GET: Enderecoes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.BuscarPor((long)id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // POST: Enderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Endereco endereco = db.BuscarPor(id);
            db.Deletar(endereco);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
