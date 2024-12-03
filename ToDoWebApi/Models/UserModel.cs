using ToDoWebApi.DTOs;

namespace ToDoWebApi.Models;

public class UserModel : ModelBase
{
    public UserModel(){}
    public UserModel(UserDTO dto)
    {
        Email = dto.Email;
        Password = dto.Password;
        Roles = dto.Roles;
    }
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] Roles { get; set; }
}