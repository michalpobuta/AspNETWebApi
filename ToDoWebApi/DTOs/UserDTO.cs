namespace ToDoWebApi.DTOs;

public class UserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] Roles { get; set; }
}