using EmailVerifyOTP.WebAPI;
using EmailVerifyOTP.WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EmailVerifyOTP.Application.Helper;
using EmailVerifyOTP.Application.Model;
using EmailVerifyOTP.Application.Interfaces;
using EmailVerifyOTP.Application.Exceptions;

namespace EmailVerifyOTP.WebAPI.Controllers
{
        [Route("api/[controller]")]
        [EnableCors("CorsPolicy")]
        [ApiController]
        public class EmailOTPController : ControllerBase
        {
            private readonly IEmailService _emailService;
            public EmailOTPController(IEmailService emailService)
            {
            _emailService = emailService;
            }

            [Route("generate")]
            [HttpPost]
            [SwaggerOperation(Summary = Constants.Generate_Const, Description = Constants.GenerateAnOtpForAnEmail_Const)]
            [SwaggerResponse(200, Type = typeof(GenerateOTPDto))]
            [SwaggerResponse(400, Type = typeof(CustomizedDetails))]
            [SwaggerResponse(404, Type = typeof(CustomizedDetails))]
            [SwaggerResponse(500, Type = typeof(CustomizedDetails))]
            public async Task<IActionResult> Generate([FromBody] GenerateOTPDto generateRequestDto)
            {
            try
            {
                    var data = await _emailService.GenerateOtpForVerify(generateRequestDto.EmailAddress);
                    return StatusCode(StatusCodes.Status200OK, data);
                }
                catch (Exception disabledException)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, disabledException.Message);
                }
                //catch (Exception ex)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError, Constants.InternalServerError_Const);
                //}
            }

            [Route("validate")]
            [HttpPut]
            [SwaggerOperation(Summary = Constants.Validate_Const, Description = Constants.ValidateAnOtp_Const)]
            [SwaggerResponse(200, Type = typeof(string))]
            [SwaggerResponse(400, Type = typeof(CustomizedDetails))]
            [SwaggerResponse(404, Type = typeof(CustomizedDetails))]
            [SwaggerResponse(500, Type = typeof(CustomizedDetails))]
            public async Task<IActionResult> Validate([FromBody] ValidateDto validateDto)
            {
                try
                {
                    if (await _emailService.ValidateOTP(validateDto.EmailAddress, validateDto.OtpCode))
                    {
                        return StatusCode(StatusCodes.Status200OK, "Validated succesfully.");
                    }
                    return StatusCode(StatusCodes.Status200OK, "Invalid Otp.");
                }
                catch (TimeOutException timeOutException)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, timeOutException.Message);
                }
                catch (NotFoundException notFoundException)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, notFoundException.Message);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, Constants.InternalServerError_Const);
                }

            }
        }
}
