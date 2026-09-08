using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.Security;
using AssessmentTest.Application.Services.PlanService;
using AssessmentTest.Application.Services.UserService;
using AssessmentTest.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PlanResponse>>> GetAll() =>
           Ok(await _planService.GetAllAsync());

        
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanResponse>> GetById(Guid id)
        {
            var plan = await _planService.GetByIdAsync(id);
            return plan is null ? NotFound() : Ok(plan);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.SuperAdmin))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlanResponse>> Create(PlanRequest request)
        {
            var created = await _planService.AddAsync(request);
            return Ok(created);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanResponse>> RemovePlanById(Guid id)
        {
            var success = await _planService.RemoveAsync(id);
            return success is false ? NotFound() : Ok(success);
        }
    }
}
