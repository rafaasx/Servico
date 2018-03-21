using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using WebServicos.Domain;
using WebServicos.Repository.Interface;

namespace WebServicos.Repository
{
    public class ServicoRepository : IRepository<Servico>
    {

        public void Add(Servico servico)
        {
            using (var db = new ServicosContext())
            {
                var context = new ValidationContext(servico, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(servico, context, results, true))
                {
                    db.Servico.Add(servico);
                    db.SaveChanges();
                    return;
                }
                string erros = string.Empty;
                foreach (var item in results)
                {
                    erros += (string.IsNullOrEmpty(erros) ? "" : "\n") + item.ErrorMessage;
                }
                throw new DbEntityValidationException(erros);
            }
        }

        public void AddRange(List<Servico> servicos)
        {
            using (var db = new ServicosContext())
            {
                var context = new ValidationContext(servicos, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(servicos, context, results, true))
                {
                    db.Servico.AddRange(servicos);
                    db.SaveChanges();
                    return;
                }
                string erros = string.Empty;
                foreach (var item in results)
                {
                    erros += (string.IsNullOrEmpty(erros) ? "" : "\n") + item.ErrorMessage;
                }
                throw new DbEntityValidationException(erros);
            }
        }

        public void Delete(int id)
        {
            using (var db = new ServicosContext())
            {
                var servico = db.Servico.FirstOrDefault(x => x.Id == id);
                if (servico != null)
                {
                    db.Servico.Remove(servico);
                    db.SaveChanges();
                }
                else
                    throw new DbEntityValidationException(string.Format("Não foi possível encontrar o Servico com código {0}", id));

            }
        }

        public Servico Get(int id)
        {
            using (var db = new ServicosContext())
            {
                return db.Servico.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Servico> List()
        {
            using (var db = new ServicosContext())
            {
                return db.Servico
                    .Include(s => s.Cliente)
                    .Include(s => s.Fornecedor)
                    .ToList();
            }
        }

        public void Update(Servico servico)
        {
            using (var db = new ServicosContext())
            {
                var context = new ValidationContext(servico, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(servico, context, results, true))
                {
                    db.Entry(servico).State = EntityState.Modified;
                    db.SaveChanges();
                    return;
                }
                string erros = string.Empty;
                foreach (var item in results)
                {
                    erros += (string.IsNullOrEmpty(erros) ? "" : "\n") + item.ErrorMessage;
                }
                throw new DbEntityValidationException(erros);
            }
        }

        public void CleanUp()
        {
            using (var db = new ServicosContext())
            {
                db.Database.ExecuteSqlCommand("DELETE from Servico;");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Servico', RESEED, 0);");
            }
        }
    }
}
