namespace ClassLibrary1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "MovieId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "MovieId");
        }
    }
}
