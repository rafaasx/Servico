namespace WebServicos.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cidade = c.String(),
                        Bairro = c.String(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fornecedor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoServico = c.Int(nullable: false),
                        Cliente_ID = c.Int(nullable: false),
                        Fornecedor_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.Cliente_ID, cascadeDelete: true)
                .ForeignKey("dbo.Fornecedor", t => t.Fornecedor_ID, cascadeDelete: true)
                .Index(t => t.Cliente_ID)
                .Index(t => t.Fornecedor_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servico", "Fornecedor_ID", "dbo.Fornecedor");
            DropForeignKey("dbo.Servico", "Cliente_ID", "dbo.Cliente");
            DropIndex("dbo.Servico", new[] { "Fornecedor_ID" });
            DropIndex("dbo.Servico", new[] { "Cliente_ID" });
            DropTable("dbo.Servico");
            DropTable("dbo.Fornecedor");
            DropTable("dbo.Cliente");
        }
    }
}
