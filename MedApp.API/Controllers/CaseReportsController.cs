using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MedApp.API.Resources;
using MedApp.API.Validators;
using MedApp.Core.Models;
using MedApp.Core.Services;

namespace MedApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseReportsController : ControllerBase
    {
        private readonly ICaseReportService _caseReportService;
        private readonly IMapper _mapper;

        public CaseReportsController(ICaseReportService caseReportService, IMapper mapper)
        {
            _mapper = mapper;
            _caseReportService = caseReportService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReportResource>> GetCaseReportById(int id)
        {
            var caseReport = await _caseReportService.GetCaseReportById(id);
            var caseReportResource = _mapper.Map<CaseReport, CaseReportResource>(caseReport);

            return Ok(caseReportResource);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReportResource>>> GetAllCaseReports()
        {
            var caseReports = await _caseReportService.GetAllWithPatient();
            var caseReportResources = _mapper.Map<IEnumerable<CaseReport>, IEnumerable<CaseReportResource>>(caseReports);

            return Ok(caseReportResources);
        }

        [HttpPost]
        public async Task<ActionResult<CaseReportResource>> CreateCaseReport([FromBody] SaveCaseReportResource saveCaseReportResource)
        {
            var validator = new SaveCaseReportResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCaseReportResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var caseReportToCreate = _mapper.Map<SaveCaseReportResource, CaseReport>(saveCaseReportResource);
            var newCaseReport = await _caseReportService.CreateCaseReport(caseReportToCreate);
            var caseReportResource = _mapper.Map<CaseReport, CaseReportResource>(newCaseReport);

            return Ok(caseReportResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CaseReportResource>> UpdateCaseReport(int id, [FromBody] SaveCaseReportResource saveCaseReportResource)
        {
            var validator = new SaveCaseReportResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCaseReportResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;
            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var caseReport = _mapper.Map<SaveCaseReportResource, CaseReport>(saveCaseReportResource);

            await _caseReportService.UpdateCaseReport(id, caseReport);

            var updatedCaseReport = await _caseReportService.GetCaseReportById(id);
            var updatedCaseReportResource = _mapper.Map<CaseReport, CaseReportResource>(updatedCaseReport);

            return Ok(updatedCaseReportResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseReport(int id)
        {
            if (id == 0)
                return BadRequest();

            var caseReport = await _caseReportService.GetCaseReportById(id);
            if (caseReport == null)
                return NotFound();

            await _caseReportService.DeleteCaseReport(caseReport);

            return NoContent();
        }
    }
}