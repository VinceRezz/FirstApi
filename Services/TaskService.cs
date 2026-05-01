using AutoMapper;
using FirstApi.Data;
using FirstApi.DTOs;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FirstApi.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<TaskDto>> GetAll(
            bool? isDone,
            string? title,
            int page,
            int pageSize,
            string sort)

        {
            var query = _context.Tasks.AsQueryable();

            //Filtres
            if (isDone.HasValue)
                query = query.Where(t => t.IsDone == isDone.Value);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(t => t.Title.Contains(title));

            //Tri
            query = sort switch
            {
                "title" => query.OrderBy(t => t.Title),
                "id" => query.OrderBy(t => t.Id),
                "date" => query.OrderBy(t => t.CreatedAt),
                _ => query.OrderBy(t => t.Id)
            };
            var totalCount = await query.CountAsync();

            //Pagination
            query = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var tasks = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TaskDto>
            {
                Items = _mapper.Map<List<TaskDto>>(tasks),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<TaskDto?> GetById(int id, int userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return null;

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> Add(CreateTaskDto dto, int userId)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.UserId = userId;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TaskDto?> Update(int id, string title, bool isDone, int userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return null;

            task.Title = title;
            task.IsDone = isDone;

            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<List<TaskDto>> GetByUser(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<TaskDto>>(tasks);
        }
    }
}
