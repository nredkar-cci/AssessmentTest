using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.Security;
using AssessmentTest.Application.Services.ClientService;
using AssessmentTest.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        private readonly ICurrentUser _currentUser;

        public ClientController(IClientService clientService, ICurrentUser currentUser)
        {
            _clientService = clientService;
            _currentUser = currentUser;
        }

        private Guid CurrentUserId => _currentUser.UserId!.Value;


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientResponse>> GetById(Guid id)
        {
            try
            {
                var ClientResponse = await _clientService.GetByIdAsync(id);
                return ClientResponse is null ? NotFound() : Ok(ClientResponse);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> Create(ClientRequest ClientRequest)
        {
            try
            {
                var created = await _clientService.ConvertUserToClient(ClientRequest, CurrentUserId);
                return created == null ? NoContent() : Ok(created);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Activate/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> ActivateClientPlan(Guid id)
        {
            try
            {
                var updated = await _clientService.ActivatePlan(id);
                return updated == null ? NoContent() : Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
