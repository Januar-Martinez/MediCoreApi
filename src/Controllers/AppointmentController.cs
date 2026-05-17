using MediCoreApi.Common;
using MediCoreApi.DTOs.Request;
using MediCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreApi.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(appointments, "Appointments retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(appointment, "Appointment retrieved successfully"));
        }

        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPatientId(int patientId)
        {
            var appointments = await _appointmentService.GetByPatientIdAsync(patientId);

            return Ok(ApiResponse<object>.Ok(appointments, "Appointments retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
        {
            var appointment = await _appointmentService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = appointment.Id },
                ApiResponse<object>.Ok(appointment, "Appointment created successfully")
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentRequest request)
        {
            var appointment = await _appointmentService.UpdateAsync(id, request);
            return Ok(ApiResponse<object>.Ok(appointment, "Appointment updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAsync(id);
            return Ok(ApiResponse<object>.Ok(null!, "Appointment deleted successfully"));
        }
    }
}
