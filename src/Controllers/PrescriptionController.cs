using MediCoreApi.Common;
using MediCoreApi.DTOs.Request;
using MediCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreApi.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var prescriptions = await _prescriptionService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(prescriptions, "Prescriptions retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var prescription = await _prescriptionService.GetByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(prescription, "Prescription retrieved successfully"));
        }

        [HttpGet("appointment/{appointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAppointmentId(int appointmentId)
        {
            var prescriptions = await _prescriptionService.GetByAppointmentIdAsync(appointmentId);

            return Ok(ApiResponse<object>.Ok(prescriptions, "Prescriptions retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionRequest request)
        {
            var prescription = await _prescriptionService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = prescription.Id },
                ApiResponse<object>.Ok(prescription, "Prescription created successfully")
            );
        }
    }
}
