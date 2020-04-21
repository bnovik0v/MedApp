using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MedApp.Core;
using MedApp.Core.Models;
using MedApp.Core.Repositories;
using NUnit.Framework;

namespace MedApp.BLL.Tests
{
    [TestFixture]
    public class CreateCaseReportTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<ICaseReportRepository> caseReportRepo,
            Dictionary<int, CaseReport> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var caseReportRepo = new Mock<ICaseReportRepository>(MockBehavior.Strict);
            var dbCollection = new Dictionary<int, CaseReport>
            {
                [26] = new CaseReport
                {
                    Id = 26,
                    PatientId = 26,
                    Diagnosis = "Delete Diagnosis"
                },
                [27] = new CaseReport
                {
                    Id = 27,
                    PatientId = 27,
                    Diagnosis = "Diagnosis"
                }
            };

            unitOfWork.SetupGet(e => e.CaseReports).Returns(caseReportRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);

            caseReportRepo.Setup(e => e.AddAsync(It.IsAny<CaseReport>()))
                     .Callback((CaseReport newCaseReport) => { dbCollection.Add(newCaseReport.Id, newCaseReport); })
                     .Returns((CaseReport _) => Task.CompletedTask);

            return (unitOfWork, caseReportRepo, dbCollection);
        }

        [Test]
        public async Task CreateCaseReport_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollection) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);
            var caseReport = new CaseReport
            {
                Id = 28,
                Diagnosis = "New Diagnosis",
                Description = "New Description"
            };

            // Act
            await service.CreateCaseReport(caseReport);

            // Assert
            Assert.IsTrue(dbCollection.ContainsKey(caseReport.Id));
        }

        [Test]
        public void CreateCaseReport_NullObject_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollection) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateCaseReport(null));
        }
    }
}