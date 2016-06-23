namespace Oratr.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Oratr.DAL.OratrContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Oratr.DAL.OratrContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Speeches.AddOrUpdate(
                speech => speech.SpeechTitle,
                new Speech { SpeechId = 1, SpeechTitle = "Declaration of Independence", SpeechBody = "When in the course of human events." },
                new Speech { SpeechId = 2, SpeechTitle = "Gettysburg Address", SpeechBody = "Four score and seven years ago, our fathers brought forth on this continent a new nation." },
                new Speech { SpeechId = 3, SpeechTitle = "Intro to Oratr", SpeechBody = "Oratr is a web app focused on improving a users public speaking ability." }
                );
        }
    }
}
