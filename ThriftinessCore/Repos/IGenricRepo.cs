using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Repos
{
    public interface IGenricRepo<T> where T : BaseEntity
    {
        Task<T> GetbyIdAsync(int id);

        Task AddAsync(T item);

        void DeleteAsync(T item);

        void Update(T item);
    }
}