using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oratr.Models;
using Oratr.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Moq;


namespace Oratr.Tests.DAL
{
    [TestClass]
    public class OratrRepositoryTest
    {
        Mock<OratrContext> mock_context { get; set; }
        OratrRepository Repo { get; set;}

        //Mock Speech Table
        Mock<DbSet<Speech>> mock_speech_table { get; set; }
        IQueryable<Speech> speech_data { get; set; }
        List<Speech> speech_datasource { get; set; }

        //Mock Result Table
        Mock<DbSet<Result>> mock_result_table { get; set; }
        IQueryable<Result> result_data { get; set; }
        List<Result> result_datasource { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<OratrContext>();

            speech_datasource = new List<Speech>();
            result_datasource = new List<Result>();

            mock_speech_table = new Mock<DbSet<Speech>>();
            mock_result_table = new Mock<DbSet<Result>>();

            Repo = new OratrRepository(mock_context.Object);

            speech_data = speech_datasource.AsQueryable();
            result_data = result_datasource.AsQueryable();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
