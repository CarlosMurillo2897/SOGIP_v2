namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_table_name : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SOGIP_Roles", newName: "SOGIP_Roles");
            RenameTable(name: "dbo.SOGIP_UserRoles", newName: "SOGIP_UserRoles");
            RenameTable(name: "dbo.SOGIP_Users", newName: "SOGIP_Users");
            RenameTable(name: "dbo.SOGIP_UserClaims", newName: "SOGIP_UserClaims");
            RenameTable(name: "dbo.SOGIP_UserLogins", newName: "SOGIP_UserLogins");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SOGIP_UserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.SOGIP_UserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.SOGIP_Users", newName: "AspNetUsers");
            RenameTable(name: "dbo.SOGIP_UserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.SOGIP_Roles", newName: "AspNetRoles");
        }
    }
}
