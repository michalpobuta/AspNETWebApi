using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController: ControllerBase
{
    private readonly IResourceService<TaskModel> _resourceService;

    public TaskController(IResourceService<TaskModel> resourceService)
    {
        _resourceService = resourceService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            return Ok(await _resourceService.GetResources());
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
            return Ok(await _resourceService.GetResourceById(id));
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
    public async Task<IActionResult> PostTask(TaskModel task)
    {
        try
        {
            return Ok(await _resourceService.Create(task));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutTask(TaskModel task)
    {
        try
        {
            await _resourceService.Update(task);
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
    public async Task<IActionResult> DeleteTask(TaskModel task)
    {
        try
        {
            await _resourceService.Delete(task);
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
}