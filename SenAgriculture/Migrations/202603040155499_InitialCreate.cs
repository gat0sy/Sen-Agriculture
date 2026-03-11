namespace AppSenAgriculture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IdCategorie = c.Int(nullable: false, identity: true),
                        LibelleCategorie = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        DescriptionCategorie = c.String(maxLength: 2000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IdCategorie);
            
            CreateTable(
                "dbo.Champs",
                c => new
                    {
                        IdChamp = c.Int(nullable: false, identity: true),
                        Superficie = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        CommuneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdChamp)
                .ForeignKey("dbo.Communes", t => t.CommuneId, cascadeDelete: true)
                .Index(t => t.CommuneId);
            
            CreateTable(
                "dbo.Communes",
                c => new
                    {
                        IdCommune = c.Int(nullable: false, identity: true),
                        CodeCommune = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        LibelleCommune = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        DepartementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCommune)
                .ForeignKey("dbo.Departements", t => t.DepartementId, cascadeDelete: true)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.Departements",
                c => new
                    {
                        IdDepartement = c.Int(nullable: false, identity: true),
                        CodeDepartement = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        LibelleDepartement = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDepartement)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        IdRegion = c.Int(nullable: false, identity: true),
                        CodeRegion = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        LibelleRegion = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IdRegion)
                .Index(t => t.CodeRegion, unique: true);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomCompletUtilisateur = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        AdresseUtilisateur = c.String(maxLength: 300, storeType: "nvarchar"),
                        EmailUtilisateur = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        TelUtilisateur = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        IdentifiantUtilisateur = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        MotDePasseUtilisateur = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        ProfessionClient = c.String(unicode: false),
                        NineaCultivateur = c.String(maxLength: 20, storeType: "nvarchar"),
                        RccmCultivateur = c.String(maxLength: 30, storeType: "nvarchar"),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MoyenDePaiements",
                c => new
                    {
                        IdMoyenDePaiement = c.Int(nullable: false, identity: true),
                        CodeMoyenDePaiement = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        LibelleMoyenDePaiement = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IdMoyenDePaiement);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        IdProduit = c.Int(nullable: false, identity: true),
                        LibelleProduit = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        DescriptionProduit = c.String(nullable: false, maxLength: 5000, storeType: "nvarchar"),
                        PrixUnitaireMin = c.Double(nullable: false),
                        IdUniteMesure = c.Int(nullable: false),
                        PrixUnitaireMax = c.Double(nullable: false),
                        CategorieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduit)
                .ForeignKey("dbo.Categories", t => t.CategorieId, cascadeDelete: true)
                .ForeignKey("dbo.UniteMesures", t => t.IdUniteMesure, cascadeDelete: true)
                .Index(t => t.IdUniteMesure)
                .Index(t => t.CategorieId);
            
            CreateTable(
                "dbo.UniteMesures",
                c => new
                    {
                        IdUnite = c.Int(nullable: false, identity: true),
                        CodeUnite = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        LibelleUnite = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IdUnite);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produits", "IdUniteMesure", "dbo.UniteMesures");
            DropForeignKey("dbo.Produits", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.Champs", "CommuneId", "dbo.Communes");
            DropForeignKey("dbo.Communes", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.Departements", "RegionId", "dbo.Regions");
            DropIndex("dbo.Produits", new[] { "CategorieId" });
            DropIndex("dbo.Produits", new[] { "IdUniteMesure" });
            DropIndex("dbo.Regions", new[] { "CodeRegion" });
            DropIndex("dbo.Departements", new[] { "RegionId" });
            DropIndex("dbo.Communes", new[] { "DepartementId" });
            DropIndex("dbo.Champs", new[] { "CommuneId" });
            DropTable("dbo.UniteMesures");
            DropTable("dbo.Produits");
            DropTable("dbo.MoyenDePaiements");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Regions");
            DropTable("dbo.Departements");
            DropTable("dbo.Communes");
            DropTable("dbo.Champs");
            DropTable("dbo.Categories");
        }
    }
}
