using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebServicos.Domain
{
    public class ServicosContext : DbContext
    {
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }

        public ServicosContext()
            : base("name=Servicos")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);


        }
    }
}
