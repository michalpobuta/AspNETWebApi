using ToDoWebApi.DTOs;

namespace ToDoWebApi.Models;

public class CategoryModel : ModelBase
{
    public CategoryModel(){}
    public CategoryModel(CategoryDTO dto, UserModel user)
    {
        Name = dto.Name;
        UserModel = user;
    }
    
    public string? Name { get; set; }
    public UserModel UserModel { get; set; } = null!;
}
