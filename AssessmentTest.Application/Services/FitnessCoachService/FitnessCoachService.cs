using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.IRepository.IFitnessCoachRepository;
using AssessmentTest.Application.IRepository.IPlanRepository;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Mappings;
using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Services.FitnessCoachService
{
    public class FitnessCoachService : IFitnessCoachService
    {
        private readonly IFitnessCoachRepository _fitnessCoachRepository;

        private readonly IUsersRepository _userRepository;

        public FitnessCoachService(IFitnessCoachRepository fitnessCoachRepository, IUsersRepository userRepository)
        {
            _fitnessCoachRepository = fitnessCoachRepository;
            _userRepository = userRepository;

        }

        #region Public Methods
        public async Task<FitnessCoachResponse?> ConvertToCoachAsync(FitnessCoachRequest fitnessCoachRequest, Guid currentUserId)
        {

            try
            {
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
            catch(Exception) {
      //ILogger will be added later.
            }
            return null;
            
        }

        public async Task<List<FitnessCoachResponse>> GetAllAsync()
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
            catch (Exception )
            { 
            
            }
            return null;
        }
        #endregion
    }
}
