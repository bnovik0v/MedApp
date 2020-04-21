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
    public class GetPatientByIdTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IPatientRepository> patientRepo, Dictionary<int, Patient> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var patientRepo = new Mock<IPatientRepository>(MockBehavior.Strict);
            var dbCollection = new Dictionary<int, Patient>
            {
                [26] = new Patient
                {
                    Id = 26,
                    FullName = "Delete Name"
                },
                [27] = new Patient
                {
                    Id = 27,
                    FullName = "Name"
                }
            };

            unitOfWork.SetupGet(e => e.Patients).Returns(patientRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);

            patientRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection[id]);

            return (unitOfWork, patientRepo, dbCollection);
        }

        [Test]
        public async Task GetPatientById_ItemExists_Success()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);

            // Act
            var patient = await service.GetPatientById(27);

            // Assert
            Assert.AreEqual(patient, dbCollection[27]);
        }

        [Test]
        public void GetPatientById_ItemDoesNotExists_KeyNotFoundException()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetPatientById(0));
        }
    }
}