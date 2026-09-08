using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.Security;
using AssessmentTest.Application.Services.FitnessCoachService;
using AssessmentTest.Application.Services.PlanService;
using AssessmentTest.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FitnessCoachController : ControllerBase
    {
        private readonly IFitnessCoachService _fitnessCoachService;

        private readonly ICurrentUser _currentUser;

        public FitnessCoachController(IFitnessCoachService fitnessCoachService, ICurrentUser currentUser)
        {
            _fitnessCoachService = fitnessCoachService;
            _currentUser = currentUser;
        }

        private Guid CurrentUserId => _currentUser.UserId!.Value;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FitnessCoachResponse>>> GetAll() =>
           Ok(await _fitnessCoachService.GetAllAsync());



        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FitnessCoachResponse>> GetById(Guid id)
        {
            var fitnessCoachResponse = await _fitnessCoachService.GetByIdAsync(id);
            return fitnessCoachResponse is null ? NotFound() : Ok(fitnessCoachResponse);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.SuperAdmin))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> Create(FitnessCoachRequest fitnessCoachRequest)
        {
            var created = await _fitnessCoachService.ConvertToCoachAsync(fitnessCoachRequest, CurrentUserId);
            return Ok(created);
        }
    }
}
