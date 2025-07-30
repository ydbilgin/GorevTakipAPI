using System.Collections.Generic;
using GorevTakipAPI.Application.DTOs;

namespace GorevTakipAPI.Application.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetAll();
        TaskDto Add(TaskDto dto);
        void Complete(int id);
        void Remove(int id);
    }
}
