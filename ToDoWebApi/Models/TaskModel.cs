using ToDoWebApi.DTOs;
using ToDoWebApi.Utils;

namespace ToDoWebApi.Models;

public class TaskModel : ModelBase
{
    public TaskModel(){}
    public TaskModel(TaskDTO dto, UserModel user)
    {
        Title = dto.Title;
        State = dto.State;
        Category = dto.Category;
        Description = dto.Description;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
        UserModel = user;
    }
    
    public string? Title { get; set; }
    public State State { get; set; }
    public CategoryModel? Category { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public UserModel UserModel { get; set; } = null!;
}

