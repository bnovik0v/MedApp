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
    public class GetCaseReportByIdTests
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

            patientRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionPatients.ContainsKey(id));

            return (unitOfWork, caseReportRepo, dbCollectionCaseReport);
        }

        [Test]
        public async Task GetCaseReportById_ItemExists_Success()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);

            // Act
            var caseReport = await service.GetCaseReportById(27);

            // Assert
            Assert.AreEqual(caseReport, dbCollectionCaseReport[27]);
        }

        [Test]
        public void GetCaseReportById_ItemDoesNotExists_KeyNotFoundException()
        {
            // Arrange
            var (unitOfWork, caseReportRepo, dbCollectionCaseReport) = GetMocks();
            var service = new CaseReportService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetCaseReportById(0));
        }
    }
}