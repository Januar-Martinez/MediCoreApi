using MediCoreApi.Common;
using MediCoreApi.DTOs.Request;
using MediCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreApi.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(patients, "Patient retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(patient, "Patient retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreatePatientRequest request)
        {
            var patient = await _patientService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = patient.Id },
                ApiResponse<object>.Ok(patient, "Patient created successfully")
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientRequest request)
        {
            var patient = await _patientService.UpdateAsync(id, request);

            return Ok(ApiResponse<object>.Ok(patient, "Patient updated successfully"));
        }
    }
}
