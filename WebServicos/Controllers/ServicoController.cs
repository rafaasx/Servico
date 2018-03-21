using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebServicos.Domain;
using WebServicos.Repository;

namespace WebServicos.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        private readonly ServicoRepository _servicoRepository = new ServicoRepository();

        // GET: Servico
        public ActionResult Index()
        {
            return View(_servicoRepository.List());
        }

        // GET: Servico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _servicoRepository.Get((int)id);
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
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
                servico.Fornecedor_ID = fornecedorRepository.List().FirstOrDefault(x => x.Email == User.Identity.Name).Id;
            if (servico.Fornecedor_ID == 0)
                ModelState.AddModelError(string.Empty, TempData["Não foi possível identificar o fornecedor."].ToString());
            if (ModelState.IsValid && servico.Fornecedor_ID > 0)
            {
                _servicoRepository.Add(servico);
                return RedirectToAction("Index", servico.Id);
            }

            return View(servico);
        }

        // GET: Servico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _servicoRepository.Get((int)id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Data,Valor,TipoServico,Cliente_ID,Fornecedor_ID")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                _servicoRepository.Update(servico);
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _servicoRepository.Get((int)id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _servicoRepository.Delete(id);
            return RedirectToAction("Index");
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
            List<Servico> servicos = _servicoRepository.List();
            if (!string.IsNullOrEmpty(filter?.Bairro))
                servicos = servicos.Where(x => x.Cliente.Bairro.Equals(filter.Bairro)).ToList();
            if (!string.IsNullOrEmpty(filter?.Cidade))
                servicos = servicos.Where(x => x.Cliente.Cidade.Equals(filter.Cidade)).ToList();
            if (!string.IsNullOrEmpty(filter?.Estado))
                servicos = servicos.Where(x => x.Cliente.Estado.Equals(filter.Estado)).ToList();
            if (filter != null && filter.Cliente > 0)
                servicos = servicos.Where(x => x.Cliente_ID.Equals(filter.Cliente)).ToList();
            if (filter != null && filter.TipoServico != 0)
                servicos = servicos.Where(x => x.TipoServico.Equals(filter.TipoServico)).ToList();
            if (filter != null && filter.ValMax > 0)
                servicos = servicos.Where(x => x.Valor <= filter.ValMax).ToList();
            if (filter != null && filter.ValMin > 0)
                servicos = servicos.Where(x => x.Valor >= filter.ValMin).ToList();

            int pageSize = 10;
            int currentPage = (page ?? 0);
            ViewBag.Pages = Math.Ceiling((decimal)servicos.Count() / pageSize);
            ViewBag.CurrentPage = currentPage;
            return View("_ReportResult", servicos.Skip(currentPage * pageSize).Take(pageSize));
        }

        // GET: Servico
        [AllowAnonymous]
        public ActionResult Stats()
        {
            return View("Stats", _servicoRepository.List());
        }

    }
}


