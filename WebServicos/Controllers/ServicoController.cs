﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebServicos.Models;
using WebServicos.Util;

namespace WebServicos.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        private ServicosContext db = new ServicosContext();

        // GET: Servico
        public async Task<ActionResult> Index()
        {
            return View(await db.Servico
                .Include(s => s.Cliente)
                .Include(s => s.Fornecedor)
                .ToListAsync());
        }

        // GET: Servico/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = await db.Servico.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Descricao,Data,Valor,TipoServico,Cliente_ID,Fornecedor_ID")] Servico servico)
        {
            var user = new ApplicationDbContext().Users.FirstOrDefault();
            servico.Fornecedor_ID = db.Fornecedor.FirstOrDefault(x => x.Email == user.Email).Id;
            if (ModelState.IsValid)
            {
                db.Servico.Add(servico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(servico);
        }

        // GET: Servico/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = await db.Servico.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Descricao,Data,Valor,TipoServico,Cliente_ID,Fornecedor_ID")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servico/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = await db.Servico.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Servico servico = await db.Servico.FindAsync(id);
            db.Servico.Remove(servico);
            await db.SaveChangesAsync();
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

        // GET: Report
        [HttpGet]
        public ActionResult Report()
        {
            return View("Report");
        }

        // GET: ReportResult
        [HttpGet]
        public ActionResult ReportResult([Bind(Include = "Cliente, Estado, Cidade, Bairro, TipoServico, ValMin, ValMax")] Util.Filter filter)
        {
            List<Servico> servicos = db.Servico
                    .Include(s => s.Cliente)
                    .Include(s => s.Fornecedor)
                    .ToList();
            if (!string.IsNullOrEmpty(filter?.Bairro))
                servicos = servicos.Where(x => x.Cliente.Bairro.Equals(filter.Bairro)).ToList();
            if (!string.IsNullOrEmpty(filter?.Cidade))
                servicos = servicos.Where(x => x.Cliente.Cidade.Equals(filter.Cidade)).ToList();
            if (!string.IsNullOrEmpty(filter?.Estado))
                servicos = servicos.Where(x => x.Cliente.Estado.Equals(filter.Estado)).ToList();
            if (filter.Cliente > 0)
                servicos = servicos.Where(x => x.Cliente_ID.Equals(filter.Cliente)).ToList();
            if ((int)filter.TipoServico != 0)
                servicos = servicos.Where(x => x.TipoServico.Equals(filter.TipoServico)).ToList();
            if (filter.ValMax > 0)
                servicos = servicos.Where(x => x.Valor <= filter.ValMax).ToList();
            if (filter.ValMax > 0)
                servicos = servicos.Where(x => x.Valor >= filter.ValMax).ToList();
            return View("_ReportResult", servicos);
        }

        // GET: Servico
        [AllowAnonymous]
        public async Task<ActionResult> Stats()
        {
            return View("Stats", await db.Servico
                .Include(s => s.Cliente)
                .Include(s => s.Fornecedor)
                .ToListAsync());
        }

    }
}


