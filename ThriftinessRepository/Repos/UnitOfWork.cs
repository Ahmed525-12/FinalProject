using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;
using ThriftinessCore.Repos;
using ThriftinessRepository.Contexts;

namespace ThriftinessRepository.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ThriftinessContext _dbContext;
        private Hashtable Repo;

        public UnitOfWork(ThriftinessContext dbContext)
        {
            _dbContext = dbContext;
            Repo = new Hashtable();
        }

        public async Task<int> CompleteAsync()
           => await _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
        => _dbContext.DisposeAsync();

        public IGenricRepo<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            if (!Repo.ContainsKey(type))
            {
                var repository = new GenricRepo<T>(_dbContext);
                Repo.Add(type, repository);
            }
            return (IGenricRepo<T>)Repo[type]!;
        }
    }
}