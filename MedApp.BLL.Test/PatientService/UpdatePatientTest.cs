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
    public class UpdatePatientTests
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
            patientRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection.ContainsKey(id));

            return (unitOfWork, patientRepo, dbCollection);
        }

        [Test]
        public async Task UpdatePatient_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);
            var patient = new Patient
            {
                FullName = "New Name"
            };

            // Act
            await service.UpdatePatient(27, patient);

            // Assert
            Assert.AreEqual((await unitOfWork.Object.Patients.GetByIdAsync(27)).FullName, patient.FullName);
        }

        [Test]
        public void UpdatePatient_EmptyFullName_InvalidDataException()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);
            var patient = new Patient()
            {
                FullName = ""
            };

            // Act + Assert
            Assert.ThrowsAsync<InvalidDataException>(async () => await service.UpdatePatient(27, patient));
        }

        [Test]
        public void UpdatePatient_NoItemForUpdate_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);
            var patient = new Patient()
            {
                FullName = "Update Name"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdatePatient(0, patient));
        }
    }
}