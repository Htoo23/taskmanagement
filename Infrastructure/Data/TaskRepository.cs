using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskmanagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskmanagementSystem.Core.Interfaces;

namespace TaskmanagementSystem.Infrastructure.Data
{
    public class TaskRepository : IRepository<TaskItem>
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TaskItem entity)
        {
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem entity)
        {
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateProgressAsync(int id, string progressStatus)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.ProgressStatus = progressStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
