using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.IRepository.IFitnessCoachRepository;
using AssessmentTest.Application.IRepository.IPlanRepository;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Mappings;
using AssessmentTest.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AssessmentTest.Application.Services.FitnessCoachService
{
    public class FitnessCoachService : IFitnessCoachService
    {
        private readonly IFitnessCoachRepository _fitnessCoachRepository;

        private readonly IUsersRepository _userRepository;

        private readonly ILogger _logger;

        public FitnessCoachService(IFitnessCoachRepository fitnessCoachRepository, IUsersRepository userRepository, ILogger<IFitnessCoachService> logger)
        {
            _fitnessCoachRepository = fitnessCoachRepository;
            _userRepository = userRepository;
            _logger = logger;

        }

        #region Public Methods
        public async Task<FitnessCoachResponse?> ConvertToCoachAsync(FitnessCoachRequest fitnessCoachRequest, Guid currentUserId)
        {

            try
            {
                var searchedFitnessCoach = (await _fitnessCoachRepository.GetAllAsync()).Where(x => x.UserId == fitnessCoachRequest.UserId || (fitnessCoachRequest.UserId == null && x.UserId == currentUserId)).ToList().SingleOrDefault();
                if (searchedFitnessCoach != null)
                {
                    throw new Exception("User is already registered as a coach"); 
                }

                // Mimicking a low level certification check instead defining special certification using file or certain criteria table.
                if (!fitnessCoachRequest.IsCertified) 
                {
                    throw new Exception("You are not certified coach");
                }

                // Rejecting the user enrolling as coach if user is doesn't atleast 2 years expirence
                if (fitnessCoachRequest.Expirence <= 2)
                {
                    throw new Exception("You are not qualified for being a coach");
                }


                var user = await _userRepository.GetByIdAsync(fitnessCoachRequest.UserId ?? currentUserId);



                if (user != null)
                {

                    FitnessCoach fitnessCoach = new FitnessCoach
                    {
                        UserId = fitnessCoachRequest.UserId ?? currentUserId,
                        CoachLevel = fitnessCoachRequest.CoachLevel,
                        IsCertified = fitnessCoachRequest.IsCertified,
                        ExpirenceYears = fitnessCoachRequest.Expirence
                    };

                    var createdFitnessCoach = await _fitnessCoachRepository.AddAsync(fitnessCoach);

                    return createdFitnessCoach.ToResponse(user);
                }

            }
            catch(Exception ex) {

                _logger.LogError(ex, "Issue occured while making user a coach");

                throw;
            }
            return null;
            
        }

        public async Task<List<FitnessCoachResponse>> GetAllAsync()
        {
            try
            {
                var fitness = await _fitnessCoachRepository.GetAllAsync();

                List<User> users = await _userRepository.GetAllAsync();

                return fitness
                    .Join(users,
                          fitnessCoach => fitnessCoach.UserId,
                          user => user.Id,
                          (fitnessCoach, user) => fitnessCoach.ToResponse(user))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured while retriving all the fitness coaches");

                throw;
            }
        }

        public async Task<FitnessCoachResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var fitnessCoach = await _fitnessCoachRepository.GetByIdAsync(id);
                var user = await _userRepository.GetByIdAsync(fitnessCoach!.UserId);

                if (fitnessCoach != null && user != null)
                {
                    return fitnessCoach.ToResponse(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured while retriving fitness coach by Id");

                throw;
            }
            return null;
        }
        #endregion
    }
}
