namespace poligonotiro.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.poligonoEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "poligonotiro.Models.poligonoEntities";
        }

        protected override void Seed(Models.poligonoEntities context) { }
    }
}
