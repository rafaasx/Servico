using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebServicos.Domain;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,Data,Valor,TipoServico,Cliente_ID,Fornecedor_ID")] Servico servico)
        {
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
                servico.Fornecedor_ID = db.Fornecedor.FirstOrDefault(x => x.Email == User.Identity.Name).Id;
            if (servico.Fornecedor_ID == 0)
                ModelState.AddModelError(string.Empty, TempData["Não foi possível identificar o fornecedor."].ToString());
            if (ModelState.IsValid && servico.Fornecedor_ID > 0)
            {
                db.Servico.Add(servico);
                int id = db.SaveChanges();
                return RedirectToAction("Index", id);
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
        public ActionResult ReportResult([Bind(Include = "Cliente, Estado, Cidade, Bairro, TipoServico, ValMin, ValMax")] Util.Filter filter, int? page)
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
            if (filter.ValMin > 0)
                servicos = servicos.Where(x => x.Valor >= filter.ValMin).ToList();

            int pageSize = 10;
            int currentPage = (page ?? 0);
            ViewBag.Pages = Math.Ceiling((decimal)servicos.Count() / pageSize);
            ViewBag.CurrentPage = currentPage;
            return View("_ReportResult", servicos.Skip(currentPage * pageSize).Take(pageSize));
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


