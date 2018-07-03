using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AluguelCarros.Aplicacao.Featues.Alugueis;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Alugueis;

namespace Uniplac.Pizzaria.Web.Controllers
{
    public class AluguelController : Controller
    {
        private IAluguelServico _aluguelServico;
        private IAluguelRepositorio aluguelRepositorio;

        public AluguelController()
        {
            aluguelRepositorio = new AluguelRepositorio();
            _aluguelServico = new AluguelServico(aluguelRepositorio);
        }

        // GET: Aluguel
        public ActionResult Index()
        {
            return View(_aluguelServico.BuscarTudo());
        }

        // GET: Aluguel/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguel aluguel = _aluguelServico.BuscarPor((long)id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }

        // GET: Aluguel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aluguel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HorasTotal,DataEntrada,DataDevolucao,ValorTotal,Pagamento,Status")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                _aluguelServico.Adicionar(aluguel);
                return RedirectToAction("Index");
            }

            return View(aluguel);
        }

        // GET: Aluguel/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguel aluguel = _aluguelServico.BuscarPor((long)id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }

        // POST: Aluguel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HorasTotal,DataEntrada,DataDevolucao,ValorTotal,Pagamento,Status")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                _aluguelServico.Editar(aluguel);
                return RedirectToAction("Index");
            }
            return View(aluguel);
        }

        // GET: Aluguel/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguel aluguel = _aluguelServico.BuscarPor((long)id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }

        // POST: Aluguel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Aluguel aluguel = _aluguelServico.BuscarPor(id);
            _aluguelServico.Deletar(aluguel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
