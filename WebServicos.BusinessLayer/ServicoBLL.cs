using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServicos.Domain;
using System.Data.Entity;

namespace WebServicos.BusinessLayer
{
    public class ServicoBLL : BLLBase<Servico>
    {
        public override int Add(Servico model)
        {
            using (var db = new ServicosContext())
            {
                db.Servico.Add(model);
                return db.SaveChanges();
            }
        }

        public override Servico Get(int id)
        {
            using (var db = new ServicosContext())
            {
                return db.Servico.FirstOrDefault(x => x.Id == id);
            }
        }

        public override List<Servico> List()
        {
            using (var db = new ServicosContext())
            {
                return db.Servico.ToList();
            }
        }

        public override int Remove(Servico model)
        {
            using (var db = new ServicosContext())
            {
                db.Servico.Remove(model);
                return db.SaveChanges();
            }
        }
    }
}
