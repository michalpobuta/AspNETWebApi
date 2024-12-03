using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.DTOs;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController: ControllerBase
{
    private readonly IResourceService<TaskModel> _taskService;
    private readonly IResourceService<UserModel> _userService;

    public TaskController(IResourceService<TaskModel> taskService, IResourceService<UserModel> userService)
    {
        _taskService = taskService;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            var resources = await _taskService.GetResources();
            return Ok(resources.Where(x => x.UserModel.Email == HttpContext.User.Identity?.Name));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Category not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        try
        {
            return Ok(await _taskService.GetResourceById(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Task not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostTask(TaskDTO task)
    {
        try
        {
            var user = (await _userService.GetResources()).FirstOrDefault(x => x.Email == HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            return Ok(await _taskService.Create(new TaskModel(task, user)));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutTask(TaskDTO task)
    {
        try
        {
            var user = (await _userService.GetResources()).FirstOrDefault(x => x.Email == HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            await _taskService.Update(new TaskModel(task,user));
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Task not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(TaskDTO task)
    {
        try
        {
            var user = (await _userService.GetResources()).FirstOrDefault(x => x.Email == HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            await _taskService.Delete(new TaskModel(task,user));
            return Ok();;
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Task not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}