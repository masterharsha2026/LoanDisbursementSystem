using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanDisbursementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET /api/loan 
        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        // GET /api/loan/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLoanByID(int id)
        {
            var loan = await _loanService.GetByIDAsync(id);

            if (loan == null)
            {
                return NotFound(new { message = "Loan not found" });
            }

            return Ok(loan);
        }


        // POST /api/loan
        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] LoanRequestDto loanRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loan = await _loanService.CreateAsync(loanRequestDto);

            if (loan == null)
            {
                return BadRequest(new { message = "Customer not found" });
            }

            return CreatedAtAction(nameof(GetLoanByID), new { id = loan.ID }, loan);
        }

        // PUT /api/loan/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLoan(int id, [FromBody] LoanUpdateDto loanUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _loanService.UpdateAsync(id, loanUpdateDto);

            if (!response.Success)
            {
                if (response.message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                {
                    return NotFound(response);
                }

                return BadRequest(response);
            }

            return Ok(response);
        }

        // DELETE /api/loan/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var response = await _loanService.DeleteAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
