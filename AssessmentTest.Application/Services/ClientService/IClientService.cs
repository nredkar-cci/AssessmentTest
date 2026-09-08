using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Services.ClientService
{
    public interface IClientService
    {
        Task<ClientResponse?> ConvertUserToClient(ClientRequest clientRequest, Guid currentUserId);

        Task<ClientResponse?> GetByIdAsync(Guid id);

        Task<ClientResponse?> ActivatePlan(Guid id);
    }
}
