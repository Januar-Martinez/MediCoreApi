using MediCoreApi.Common;
using MediCoreApi.DTOs.Request;
using MediCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreApi.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(doctors, "Doctors retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(doctor, "Doctor retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateDoctorRequest request)
        {
            var doctor = await _doctorService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = doctor.Id },
                ApiResponse<object>.Ok(doctor, "Doctor created successfully")
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorRequest request)
        {
            var doctor = await _doctorService.UpdateAsync(id, request);
            return Ok(ApiResponse<object>.Ok(doctor, "Doctor updated successfully"));
        }
    }
}
