using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Models;

namespace StudentManagement.API.Services.Interfaces;

public interface IMarkService
{
    Task<MarkResponse?> GetMarkAsync(GetMarkRequest markRequest);
    Task<MarkResponse?> RecordMarkAsync(MarkRequest markRequest);
}