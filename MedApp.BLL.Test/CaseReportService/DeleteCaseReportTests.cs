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
    public class DeleteCaseReportTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<ICaseReportRepository> caseReportRepo,
            Dictionary<int, CaseReport> dbCollectionCaseReport) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var caseReportRepo = new Mock<ICaseReportRepository>(MockBehavior.Strict);
            var patientRepo = new Mock<IPatientRepository>(MockBehavior.Strict);
            var dbCollectionCaseReport = new Dictionary<int, CaseReport>
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

            var dbCollectionPatients = new Dictionary<int, Patient>
            {
                [26] = new Patient
                {
                    Id = 26,
                    FullName = "Name"
                },
                [27] = new Patient
                {
                    Id = 27,
                    FullName = "Other Name"
                }
            };

            unitOfWork.SetupGet(e => e.CaseReports).Returns(caseReportRepo.Object);
            unitOfWork.SetupGet(e => e.Patients).Returns(patientRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);

            caseReportRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                     .ReturnsAsync((int id) => dbCollectionCaseReport.ContainsKey(id));
            caseReportRepo.Setup(e => e.Remove(It.IsAny<CaseReport>()))
                     .Callback((CaseReport newCaseReport) => { dbCollectionCaseReport.Remove(newCaseReport.Id); });

            patientRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionPatients.ContainsKey(id));
            patientRepo.Setup(e => e.Remove(It.IsAny<Patient>()))
                      .Callback((Patient newPatient) => { dbCollectionPatients.Remove(newPatient.Id); });

            return (unitOfWork, caseReportRepo, dbCollectionCaseReport);
        }

        [Test]
        public async Task DeleteCaseReport_TargetItem_Success()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);
            var caseReport = new CaseReport
            {
                Id = 26,
                Diagnosis = "Delete Diagnosis"
            };

            // Act
            await service.DeleteCaseReport(caseReport);

            // Assert
            Assert.IsFalse(dbCollectionCaseReport.ContainsKey(26));
        }

        [Test]
        public void DeleteCaseReport_ItemDoesNotExists_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);
            var caseReport = new CaseReport
            {
                Id = 0,
                Diagnosis = "Delete Diagnosis"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteCaseReport(caseReport));
        }
    }
}