using AErmilov.NumbersIntoWords.Api.Contracts;
using AErmilov.NumbersIntoWords.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Numbers into words conversion controller
    /// </summary>
    [ApiController]
    [Route("numbers/")]
    public sealed class NumbersIntoWordsController : ControllerBase
    {
        private readonly INumbersIntoWordsService numbersIntoWordsService;

        public NumbersIntoWordsController(INumbersIntoWordsService numbersIntoWordsService)
        {
            this.numbersIntoWordsService = numbersIntoWordsService;
        }

        /// <summary>
        /// Perform number into words conversion
        /// </summary>
        [HttpGet("{number}/words", Name = "ConvertNumberIntoWords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NumberAsWordsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult ConvertNumberIntoWords([FromRoute] decimal number)
        {
            return Ok(new NumberAsWordsResponse
            {
                Number = number,
                Words = numbersIntoWordsService.NumberIntoWords(number),
            });
        }
    }
}