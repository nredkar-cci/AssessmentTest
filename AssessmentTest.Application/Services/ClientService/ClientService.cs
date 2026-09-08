using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.IRepository.IClientRepository;
using AssessmentTest.Application.IRepository.IFitnessCoachRepository;
using AssessmentTest.Application.IRepository.IPlanRepository;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Mappings;
using AssessmentTest.Domain.Entities;
using AssessmentTest.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        private readonly IUsersRepository _userRepository;

        private readonly IPlanRepository _planRepository;

        private readonly IFitnessCoachRepository _fitnessCoachRepository;

        private readonly ILogger _logger;

        public ClientService(IClientRepository clientRepository,
            IUsersRepository userRepository,
            IPlanRepository planRepository,
            IFitnessCoachRepository fitnessCoachRepository,
            ILogger<IClientService> logger)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _planRepository = planRepository;
            _fitnessCoachRepository = fitnessCoachRepository;
            _logger = logger;

        }

        #region Public Methods
        public async Task<ClientResponse?> ActivatePlan(Guid id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client != null)
                {
                    var user = await _userRepository.GetByIdAsync(client.UserId);


                    var fitnessCoach = await _fitnessCoachRepository.GetByIdAsync(client.FitnessCoachId ?? new Guid());

                    if (fitnessCoach == null)
                    {
                        return null;
                    }

                    var coach = await _userRepository.GetByIdAsync(fitnessCoach!.UserId);

                    var plan = await _planRepository.GetByIdAsync(client.PlanId ?? new Guid());

                    var expiryDateAdding = DateTime.UtcNow;

                    switch (plan!.PlanType)
                    { 
                        case PlanType.PerDay: expiryDateAdding.AddDays(1); break;

                        case PlanType.Monthly: expiryDateAdding.AddMonths(1);break;

                        case PlanType.Yearly: expiryDateAdding.AddYears(1); break;

                    }

                    client.PlanExpiry = expiryDateAdding;

                    await _clientRepository.UpdateAsync(client);

                    if (user != null && coach != null && plan != null && client != null)
                    {
                        return client.ToResponse(user, coach, plan);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured while activating the plan");
                throw;
            }
            return null;
        }

        public async Task<ClientResponse?> ConvertUserToClient(ClientRequest clientRequest, Guid currentUserId)
        {
            try
            {
                var searchedClient = (await _clientRepository.GetAllAsync()).Where(x => x.UserId == clientRequest.UserId || (clientRequest.UserId == null && x.UserId == currentUserId)).ToList().SingleOrDefault();

                if (searchedClient != null)
                {
                    throw new Exception("This user already has a plan");
                }

                var user = await _userRepository.GetByIdAsync(clientRequest.UserId ?? currentUserId);

                var fitnessCoach = await _fitnessCoachRepository.GetByIdAsync(clientRequest.FitnessCoachId);

                var coach = await _userRepository.GetByIdAsync(fitnessCoach!.UserId);

                var plan = await _planRepository.GetByIdAsync(clientRequest.PlanId);

                if (user != null && coach != null && plan != null)
                {

                    Client client = new Client
                    {
                        UserId = clientRequest.UserId ?? currentUserId,
                        PlanId = clientRequest.PlanId,
                        PlanStatus = PlanStatus.NotStarted,
                        FitnessCoachId = clientRequest.FitnessCoachId
                    };

                    var createdClient = await _clientRepository.AddAsync(client);
                    
                
                    return createdClient.ToResponse(user,coach, plan);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured while making user a client");
                throw;
            }

            return null;

        }

        public async Task<ClientResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client != null)
                {
                    var user = await _userRepository.GetByIdAsync(client.Id);


                    var fitnessCoach = await _fitnessCoachRepository.GetByIdAsync(client.FitnessCoachId ?? new Guid());

                    if (fitnessCoach == null) 
                    {
                        return null;
                    }
                    
                    var coach = await _userRepository.GetByIdAsync(fitnessCoach!.UserId);

                    var plan = await _planRepository.GetByIdAsync(client.PlanId ?? new Guid());

                    if (user != null && coach != null && plan != null && client != null)
                    {
                        return client.ToResponse(user, coach, plan);
                    }
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured while retriving client using ID");
                throw;
            }
            return null;
        }
        #endregion
    }
}
