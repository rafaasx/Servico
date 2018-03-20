using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using WebServicos.Domain;
using WebServicos.Repository.Interface;

namespace WebServicos.Repository
{
    public class FornecedorRepository : IRepository<Fornecedor>
    {

        public void Add(Fornecedor fornecedor)
        {
            using (var db = new ServicosContext())
            {
                var context = new ValidationContext(fornecedor, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(fornecedor, context, results, true))
                {
                    db.Fornecedor.Add(fornecedor);
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

        public void AddRange(List<Fornecedor> fornecedores)
        {
            using (var db = new ServicosContext())
            {
                var context = new ValidationContext(fornecedores, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(fornecedores, context, results, true))
                {
                    db.Fornecedor.AddRange(fornecedores);
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
                var fornecedor = db.Fornecedor.FirstOrDefault(x => x.Id == id);
                if (fornecedor != null)
                {
                    db.Fornecedor.Remove(fornecedor);
                    db.SaveChanges();
                }
                else
                    throw new DbEntityValidationException(string.Format("Não foi possível encontrar o fornecedor com código {0}", id));

            }
        }

        public Fornecedor Get(int id)
        {
            using (var db = new ServicosContext())
            {
                return db.Fornecedor.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Fornecedor> List()
        {
            using (var db = new ServicosContext())
            {
                return db.Fornecedor.ToList();
            }
        }

        public void Update(Fornecedor fornecedor)
        {
            try
            {
                using (var db = new ServicosContext())
                {
                    var context = new ValidationContext(fornecedor, serviceProvider: null, items: null);
                    var results = new List<ValidationResult>();
                    if (Validator.TryValidateObject(fornecedor, context, results, true))
                    {
                        db.Entry(fornecedor).State = EntityState.Modified;
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
            catch (Exception)
            {
                throw;
            }
        }

        public void CleanUp()
        {
            using (var db = new ServicosContext())
            {
                db.Database.ExecuteSqlCommand("DELETE from Fornecedor;");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Fornecedor', RESEED, 0);");
            }
        }
    }
}
