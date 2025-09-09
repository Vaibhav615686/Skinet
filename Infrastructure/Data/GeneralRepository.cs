using System;
using System.Dynamic;
using API.Models;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GeneralRepository<T>(AppDbContext context) : IGeneralRepository<T> where T : BaseEntity
{
    public async Task<ServerResponse<T?>> GetByIdAsync(int Id)
    {
        ServerResponse<T?> response = new ServerResponse<T?>();
        var data = await context.Set<T>().FindAsync(Id);
        response.IsSuccess = true;
        response.Data = data;
        if (data == null)
        {
            response.Message = "Record not found";
        }
        else
        {
            response.Message = "Record Found Successfully";
        }
        return response;
    }

    public async Task<ServerResponse<T>> Add(T entity)
    {
        ServerResponse<T> response = new ServerResponse<T>();
        await context.Set<T>().AddAsync(entity);
        if (await context.SaveChangesAsync() > 0)
        {
            response.IsSuccess = true;
            response.Data = entity;
            response.Message = "Data added successfully";
        }
        else
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
        }
        return response;
    }

    public async Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
    {
         ServerResponse<IEnumerable<TResult>> response = new ServerResponse<IEnumerable<TResult>>();
        var data = await ApplySpecification(spec).ToListAsync();
        response.IsSuccess = true;
        response.Data = data;
        if (data.Count == 0)
        {
            response.Message = "Records not found";
        }
        else
        {
            response.Message = "Records Found Successfully";
        }
        return data;
    }
    public async Task<ServerResponse<T>> Delete(T entity)
    {
        ServerResponse<T> response = new ServerResponse<T>();
        context.Set<T>().Remove(entity);
        if (await context.SaveChangesAsync() > 0)
        {
            response.IsSuccess = true;
            response.Data = entity;
            response.Message = "Data deleted successfully";
        }
        else
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
        }
        return response;
    }

    public async Task<ServerResponse<IEnumerable<T>>> ListAllAsync()
    {
        ServerResponse<IEnumerable<T>> response = new ServerResponse<IEnumerable<T>>();
        var data = await context.Set<T>().ToListAsync();
        response.IsSuccess = true;
        response.Data = data;
        if (data == null)
        {
            response.Message = "Records not found";
        }
        else
        {
            response.Message = "Records Found Successfully";
        }
        return response;
    }

    public async Task<ServerResponse<T>> Update(int Id, T entity)
    {
        ServerResponse<T> response = new ServerResponse<T>();
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        if (await context.SaveChangesAsync() > 0)
        {
            response.IsSuccess = true;
            response.Data = entity;
            response.Message = "Data updated successfully";
        }
        else
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
        }
        return response;
    }

    public async Task<bool> Exists(int Id)
    {
        return await context.Set<T>().AnyAsync(x => x.Id == Id);
    }
    public async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec)
    {
        ServerResponse<IEnumerable<T>> response = new ServerResponse<IEnumerable<T>>();
        var data = await ApplySpecification(spec).ToListAsync();
        response.IsSuccess = true;
        response.Data = data;
        if (data.Count == 0)
        {
            response.Message = "Records not found";
        }
        else
        {
            response.Message = "Records Found Successfully";
        }
        return data;
    }
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvalutor<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
    }
        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T,TResult> spec)
    {
        return SpecificationEvalutor<T>.GetQuery<T,TResult>(context.Set<T>().AsQueryable(), spec);
    }

   public  async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = context.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
        return await query.CountAsync();
    }
}
