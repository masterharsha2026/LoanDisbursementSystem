using LoanDisbursementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using LoanDisbursementSystem.Services;
using LoanDisbursementSystem.DTO;
using Microsoft.AspNetCore.Mvc;


namespace LaonDisbursementTests
{
    [TestFixture]
    internal class LoadnControllerTests
    {

        private Mock<ILoanService> _loanServiceMock = new Mock<ILoanService>();



        [Test]
        public async Task GetLoanByIDWhenInvokedWithValidInput_return200()
        {
            LoanController loanController = new LoanController(_loanServiceMock.Object);


            var loan = new LoanResponseDto()
            {
                ID = 1
            };

            _loanServiceMock.Setup(x => x.GetByIDAsync(1)).ReturnsAsync(loan);

            IActionResult result = await loanController.GetLoanByID(1);

            var ok = result as OkObjectResult;

            Assert.AreEqual(201, ok.StatusCode);
        }


    }
}
