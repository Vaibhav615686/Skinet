using System;
using API.Models;
using Core.Entities;
namespace Core.Interfaces;

public interface IGeneralRepository<T> where T : BaseEntity
{
    Task<ServerResponse<T?>> GetByIdAsync(int Id);
    Task<ServerResponse<IEnumerable<T>>> ListAllAsync();

    Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);

    Task<ServerResponse<T>> Add(T entity);

    Task<ServerResponse<T>> Update(int Id, T entity);

    Task<ServerResponse<T>> Delete(T entity);

    Task<bool> Exists(int Id);

    Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);
    Task<int> CountAsync(ISpecification<T> spec);
}

public class BaseEntity
{
    public int Id { get; set; }
}