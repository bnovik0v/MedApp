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
    public class DeletePatientTests
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

            patientRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection.ContainsKey(id));
            patientRepo.Setup(e => e.Remove(It.IsAny<Patient>()))
                      .Callback((Patient newPatient) => { dbCollection.Remove(newPatient.Id); });

            return (unitOfWork, patientRepo, dbCollection);
        }

        [Test]
        public async Task DeletePatient_TargetItem_Success()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);
            var patient = new Patient
            {
                Id = 26,
                FullName = "Delete Name"
            };

            // Act
            await service.DeletePatient(patient);

            // Assert
            Assert.IsFalse(dbCollection.ContainsKey(26));
        }

        [Test]
        public void DeletePatient_ItemDoesNotExists_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, patientRepo, dbCollection) = GetMocks();
            var service = new PatientService(unitOfWork.Object);
            var patient = new Patient
            {
                Id = 0,
                FullName = "Delete Name"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeletePatient(patient));
        }
    }
}