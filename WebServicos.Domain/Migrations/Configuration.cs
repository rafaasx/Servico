namespace WebServicos.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static WebServicos.Domain.Enum;

    internal sealed class Configuration : DbMigrationsConfiguration<ServicosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebServicos.Domain.ServicosContext";
        }

        protected override void Seed(ServicosContext servicosContext)
        {
            base.Seed(servicosContext);
        }
    }
}
