using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.IRepository.IClientRepository
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(Guid id);

        Task<List<Client>> GetAllAsync();

        Task<Client> AddAsync(Client client);
    }
}
