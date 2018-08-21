namespace MyMVVMParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vacancies", "Contact");
            DropColumn("dbo.Vacancies", "Number");
            DropColumn("dbo.Vacancies", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vacancies", "Type", c => c.String());
            AddColumn("dbo.Vacancies", "Number", c => c.String());
            AddColumn("dbo.Vacancies", "Contact", c => c.String());
        }
    }
}
