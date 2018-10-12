namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SOGIP_UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.SOGIP_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.SOGIP_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SOGIP_Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.SOGIP_UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SOGIP_UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.SOGIP_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_UserRoles", "UserId", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_UserLogins", "UserId", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_UserClaims", "UserId", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_UserRoles", "RoleId", "dbo.SOGIP_Roles");
            DropIndex("dbo.SOGIP_UserLogins", new[] { "UserId" });
            DropIndex("dbo.SOGIP_UserClaims", new[] { "UserId" });
            DropIndex("dbo.SOGIP_Users", "UserNameIndex");
            DropIndex("dbo.SOGIP_UserRoles", new[] { "RoleId" });
            DropIndex("dbo.SOGIP_UserRoles", new[] { "UserId" });
            DropIndex("dbo.SOGIP_Roles", "RoleNameIndex");
            DropTable("dbo.SOGIP_UserLogins");
            DropTable("dbo.SOGIP_UserClaims");
            DropTable("dbo.SOGIP_Users");
            DropTable("dbo.SOGIP_UserRoles");
            DropTable("dbo.SOGIP_Roles");
        }
    }
}
