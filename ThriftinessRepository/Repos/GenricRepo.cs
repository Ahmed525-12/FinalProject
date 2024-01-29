using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;
using ThriftinessCore.Repos;
using ThriftinessRepository.Contexts;

namespace ThriftinessRepository.Repos
{
    public class GenricRepo<T> : IGenricRepo<T> where T : BaseEntity
    {
        private readonly ThriftinessContext _dbContext;

        public GenricRepo(ThriftinessContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item);

        public void DeleteAsync(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<T?> GetbyIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T item)
        {
            _dbContext.Update(item);
        }
    }
}