using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanDisbursementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DisbursementController : ControllerBase
    {
        private readonly IDisbursementService _disbursementService;

        public DisbursementController(IDisbursementService disbursementService)
        {
            _disbursementService = disbursementService;
        }


        // GET /api/disbursement

        [HttpGet]
        public async Task<IActionResult> GetAllDisbursement()
        {
            var disbursements = await _disbursementService.GetAllAsync();
            return Ok(disbursements);
        }

        // GET /api/disbursement/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDisbursementByID(int id)
        {
            var disbursement = await _disbursementService.GetByIDAsync(id);

            if (disbursement == null)
            {
                return NotFound(new { message = "Disbursement not found" });
            }

            return Ok(disbursement);
        }


        // POST /api/disbursement
        [HttpPost]
        public async Task<IActionResult> DisburseLoan([FromBody] DisbursementRequestDto disbursementRequestDto)
        {
            var response = await _disbursementService.DisburseLoanAsync(disbursementRequestDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


    }
}
