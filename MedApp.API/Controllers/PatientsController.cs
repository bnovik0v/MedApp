using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MedApp.API.Resources;
using MedApp.Core.Models;
using MedApp.Core.Services;

namespace MedApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<SavePatientResource> _validator;

        public PatientsController(IPatientService patientService, IMapper mapper, AbstractValidator<SavePatientResource> validator)
        {
            _mapper = mapper;
            _patientService = patientService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResource>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            var patientResources = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientResource>>(patients);

            return Ok(patientResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResource>> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientById(id);
            var patientResource = _mapper.Map<Patient, PatientResource>(patient);

            return Ok(patientResource);
        }

        [HttpPost]
        public async Task<ActionResult<PatientResource>> CreatePatient([FromBody] SavePatientResource savePatientResource)
        {
            var validationResult = await _validator.ValidateAsync(savePatientResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var patientToCreate = _mapper.Map<SavePatientResource, Patient>(savePatientResource);
            var newPatient = await _patientService.CreatePatient(patientToCreate);
            var patientResource = _mapper.Map<Patient, PatientResource>(newPatient);

            return Ok(patientResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientResource>> UpdatePatient(int id, [FromBody] SavePatientResource savePatientResource)
        {
            var validationResult = await _validator.ValidateAsync(savePatientResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var patient = _mapper.Map<SavePatientResource, Patient>(savePatientResource);

            await _patientService.UpdatePatient(id, patient);

            var updatedPatient = await _patientService.GetPatientById(id);
            var updatedPatientResource = _mapper.Map<Patient, PatientResource>(updatedPatient);

            return Ok(updatedPatientResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientService.GetPatientById(id);

            await _patientService.DeletePatient(patient);

            return NoContent();
        }
    }
}