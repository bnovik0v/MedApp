using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using MedApp.Core;
using MedApp.Core.Models;
using MedApp.Core.Repositories;
using NUnit.Framework;

namespace MedApp.BLL.Tests
{
    [TestFixture]
    public class UpdateCaseReportTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<ICaseReportRepository> caseReportRepo, Dictionary<int, CaseReport> dbCollectionCaseReport) GetMocks()
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

            caseReportRepo.Setup(e => e.GetWithPatientByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionCaseReport[id]);
            caseReportRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionCaseReport.ContainsKey(id));

            patientRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionPatients[id]);
            patientRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionPatients.ContainsKey(id));

            return (unitOfWork, caseReportRepo, dbCollectionCaseReport);
        }

        [Test]
        public async Task UpdateCaseReport_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);
            var caseReport = new CaseReport
            {
                PatientId = 27,
                Diagnosis = "New Diagnosis"
            };

            // Act
            await service.UpdateCaseReport(27, caseReport);

            // Assert
            Assert.AreEqual((await unitOfWork.Object.CaseReports.GetWithPatientByIdAsync(27)).Diagnosis, caseReport.Diagnosis);
        }

        [Test]
        public void UpdateCaseReport_EmptyFullName_InvalidDataException()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);
            var caseReport = new CaseReport()
            {
                Diagnosis = ""
            };

            // Act + Assert
            Assert.ThrowsAsync<InvalidDataException>(async () => await service.UpdateCaseReport(27, caseReport));
        }

        [Test]
        public void UpdateCaseReport_NoItemForUpdate_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);
            var caseReport = new CaseReport()
            {
                Diagnosis = "Update Track"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateCaseReport(0, caseReport));
        }
    }
}