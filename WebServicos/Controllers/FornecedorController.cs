using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebServicos.Models;
using WebServicos.Domain;
using WebServicos.Repository;

namespace WebServicos.Controllers
{
    [Authorize]
    public class FornecedorController : Controller
    {
        private FornecedorRepository fornecedorRepository = new FornecedorRepository();

        // GET: Fornecedor
        public ActionResult Index()
        {
            return View(fornecedorRepository.List());
        }

        // GET: Fornecedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = fornecedorRepository.Get((int)id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                fornecedorRepository.Add(fornecedor);
                return RedirectToAction("Index");
            }

            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = fornecedorRepository.Get((int)id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                fornecedorRepository.Update(fornecedor);
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = fornecedorRepository.Get((int)id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = fornecedorRepository.Get((int)id);
            fornecedorRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
