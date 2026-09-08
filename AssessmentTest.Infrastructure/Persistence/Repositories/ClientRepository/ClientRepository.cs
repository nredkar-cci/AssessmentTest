using AssessmentTest.Application.IRepository.IBaseRepository;
using AssessmentTest.Application.IRepository.IClientRepository;
using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AssessmentTest.Infrastructure.Persistence.Repositories.ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IBaseRepository<Client> _baseRepository;

        public ClientRepository(IBaseRepository<Client> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<Client?> GetByIdAsync(Guid id) => _baseRepository.GetByIdAsync(id);

        public Task<List<Client>> GetAllAsync() => _baseRepository.GetAllAsync();

        public Task<Client> AddAsync(Client client) => _baseRepository.AddAsync(client);
    }
}
