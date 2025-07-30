using System.Collections.Generic;
using System.Linq;
using GorevTakipAPI.Application.DTOs;
using GorevTakipAPI.Domain.Models;
using GorevTakipAPI.Infrastructure.Persistence;

namespace GorevTakipAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _repo;
        public TaskService(TaskRepository repo) => _repo = repo;

        public IEnumerable<TaskDto> GetAll() =>
            _repo.GetAll()
                 .Select(e => new TaskDto
                 {
                     Id = e.Id,
                     Title = e.Title,
                     Description = e.Description,
                     IsCompleted = e.IsCompleted
                 });

        public TaskDto Add(TaskDto dto)
        {
            var entity = new TodoTask
            {
                Title = dto.Title,
                Description = dto.Description
            };
            _repo.Add(entity);
            dto.Id = entity.Id;
            return dto;
        }

        public void Complete(int id) => _repo.MarkCompleted(id);
        public void Remove(int id) => _repo.Delete(id);
    }
}
