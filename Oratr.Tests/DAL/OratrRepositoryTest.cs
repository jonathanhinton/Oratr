using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oratr.DAL;
using Moq;
using System.Collections.Generic;
using Oratr.Models;
using System.Linq;
using System.Data.Entity;


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

        void ConnectMocksToDatastore()
        {
            // Telling fake DbSet to use our datasource like something Queryable
            mock_speech_table.As<IQueryable<Speech>>().Setup(m => m.GetEnumerator()).Returns(speech_data.GetEnumerator());
            mock_speech_table.As<IQueryable<Speech>>().Setup(m => m.ElementType).Returns(speech_data.ElementType);
            mock_speech_table.As<IQueryable<Speech>>().Setup(m => m.Expression).Returns(speech_data.Expression);
            mock_speech_table.As<IQueryable<Speech>>().Setup(m => m.Provider).Returns(speech_data.Provider);

            // Tell mocked OratrContext to use our fully mocked Datasource. (List<Speech>)
            mock_context.Setup(m => m.Speeches).Returns(mock_speech_table.Object);

            // Telling fake DbSet to use our datasource like something Queryable
            mock_result_table.As<IQueryable<Result>>().Setup(m => m.GetEnumerator()).Returns(result_data.GetEnumerator());
            mock_result_table.As<IQueryable<Result>>().Setup(m => m.ElementType).Returns(result_data.ElementType);
            mock_result_table.As<IQueryable<Result>>().Setup(m => m.Expression).Returns(result_data.Expression);
            mock_result_table.As<IQueryable<Result>>().Setup(m => m.Provider).Returns(result_data.Provider);

            // Tell mocked OratrContext to use our fully mocked Datasource. (List<Result>)
            mock_context.Setup(m => m.Results).Returns(mock_result_table.Object);

            // Hijack the call to add information to the database, instead add it in the list so as to mock Add aciton
            mock_result_table.Setup(m => m.Add(It.IsAny<Result>())).Callback((Result result) => result_datasource.Add(result));
            mock_speech_table.Setup(m => m.Add(It.IsAny<Speech>())).Callback((Speech speech) => speech_datasource.Add(speech));
        }

        [TestMethod]
        public void RepoEnsureICanCreateAnInstance()
        {
            Assert.IsNotNull(Repo);
        }

        [TestMethod]
        public void RepoEnsureIsUsingContext()
        {
            Assert.IsNotNull(Repo.context);
        }

        [TestMethod]
        public void RepoEnsureThereAreNoSpeeches()
        {
            // Arrange
            ConnectMocksToDatastore();

            // Act
            List<Speech> list_of_speeches = Repo.GetSpeeches();
            List<Speech> expected = new List<Speech>();

            // Assert
            Assert.AreEqual(expected.Count, list_of_speeches.Count);
        }

        [TestMethod]
        public void RepoEnsureSpeechCountIsZero()
        {
            // Arrange
            ConnectMocksToDatastore();

            // Act
            int expected = 0;
            int actual = Repo.GetSpeechCount();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoEnsureICanAddSpeech()
        {
            // Arrange
            ConnectMocksToDatastore();

            // Act
            ApplicationUser created_by = new ApplicationUser();
            created_by.Id = "fake_user_id";
            string speechTitle = "Some Title";
            string speechBody = "Some text that is a lot longer in order to mock the string that represents the body of the text";

            Repo.AddSpeech(speechTitle, speechBody, created_by);

            // Assert
            Assert.AreEqual(1, Repo.GetSpeechCount());
        }

        [TestMethod]
        public void RepoEnsureICanGetSpeeches()
        {
            // Arrange
            Speech speech1 = new Speech { SpeechId = 1, SpeechTitle = "some title", SpeechBody = "some body" };
            Speech speech2 = new Speech { SpeechId = 2, SpeechTitle = "some title", SpeechBody = "some body" };
            Speech speech3 = new Speech { SpeechId = 3, SpeechTitle = "some title", SpeechBody = "some body" };
            speech_datasource.Add(speech1);
            speech_datasource.Add(speech2);
            speech_datasource.Add(speech3);

            ConnectMocksToDatastore();
            // Act
            List<Speech> speeches = Repo.GetSpeeches();

            // Assert
            Assert.AreEqual(3, speeches.Count);
        }

        [TestMethod]
        public void RepoEnsureICanGetSpecificSpeech()
        {
            // Arrange
            Speech speech1 = new Speech { SpeechId = 1, SpeechTitle = "some title", SpeechBody = "some body" };
            Speech speech2 = new Speech { SpeechId = 2, SpeechTitle = "some title", SpeechBody = "some body" };
            Speech speech3 = new Speech { SpeechId = 3, SpeechTitle = "some title", SpeechBody = "some body" };
            speech_datasource.Add(speech1);
            speech_datasource.Add(speech2);
            speech_datasource.Add(speech3);

            ConnectMocksToDatastore();

            // Act
            Speech found_speech = Repo.GetSpeech(2);

            // Assert
            Assert.IsNotNull(found_speech);
            Assert.AreEqual(found_speech, speech2);
        }

        [TestMethod]
        public void RepoEnsureICanCalculateTargetDeliveryTime()
        {
            // Arrange
            Speech speech1 = new Speech { SpeechId = 1, SpeechTitle = "Declaration of Independence", SpeechBody = "When in the course of human events." };
            Speech speech2 = new Speech { SpeechId = 2, SpeechTitle = "Gettysburg Address", SpeechBody = "Four score and seven years ago, our fathers brought forth on this continent a new nation." };
            Speech speech3 = new Speech { SpeechId = 3, SpeechTitle = "some title", SpeechBody = "some body" };
            speech_datasource.Add(speech1);
            speech_datasource.Add(speech2);
            speech_datasource.Add(speech3);

            ConnectMocksToDatastore();
            // Act
            Speech found_speech = Repo.GetSpeech(2);
            ApplicationUser some_user = new ApplicationUser();
            some_user.UserWPM = 16;
            Repo.CalculateDeliveryTime(some_user, found_speech);

            TimeSpan actual = found_speech.TargetDeliveryTime;
            TimeSpan expected = TimeSpan.FromMinutes(1); ;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoEnsureICanDeleteASpeech()
        {
            // Arrange
            Speech speech1 = new Speech { SpeechId = 1, SpeechTitle = "Declaration of Independence", SpeechBody = "When in the course of human events." };
            Speech speech2 = new Speech { SpeechId = 2, SpeechTitle = "Gettysburg Address", SpeechBody = "Four score and seven years ago, our fathers brought forth on this continent a new nation." };
            Speech speech3 = new Speech { SpeechId = 3, SpeechTitle = "some title", SpeechBody = "some body" };
            speech_datasource.Add(speech1);
            speech_datasource.Add(speech2);
            speech_datasource.Add(speech3);

            ConnectMocksToDatastore();
            mock_speech_table.Setup(m => m.Remove(It.IsAny<Speech>())).Callback((Speech speech) => speech_datasource.Remove(speech));

            // Act
            Repo.RemoveSpeech(3);

            // Assert
            int expected_count = 2;
            Assert.AreEqual(expected_count, Repo.GetSpeechCount());

        }

        [TestMethod]
        public void RepoEnsureICanCalculateUserWPM()
        {
            // Arrange
            ApplicationUser some_user = new ApplicationUser();
            some_user.Id = "fake_user_id";

            // Act
            string oneMinuteWordCount = "this is the result of someone who has just spoken for one minute about a topic that they are intimately familiar with";
            Repo.CalculateUserWPM(some_user, oneMinuteWordCount);
            int expected_wpm = 22;

            // Assert
            Assert.AreEqual(expected_wpm, some_user.UserWPM);
        }
    }
}
