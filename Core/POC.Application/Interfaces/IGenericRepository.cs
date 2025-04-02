using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace POC.Application.Interfaces
{
    public interface IGenericRepository//<T> where T : BaseEntity
    {
        IConfigurationSection GetConfigurationSection(string Key);

        //Task<T> GetByIdAsync<T>(int id);
        //Task<IReadOnlyList<T>> GetAllAsync<T>();
        //Task<IReadOnlyList<T>> GetPagedReponseAsync<T>(int pageNumber, int pageSize);

        //Task<T> AddAsync<T>(T entity);
        //Task UpdateAsync<T>(T entity);
        //Task DeleteAsync<T>(T entity);
    }
}
