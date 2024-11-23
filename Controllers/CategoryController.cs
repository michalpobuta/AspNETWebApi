using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController: ControllerBase
{
    private readonly IResourceService<CategoryModel> _resourceService;

    public CategoryController(IResourceService<CategoryModel> resourceService)
    {
        _resourceService = resourceService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
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
    public async Task<IActionResult> GetCategory(int id)
    {
        try
        {
            return Ok(await _resourceService.GetResourceById(id));
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

    [HttpPost]
    public async Task<IActionResult> PostCategory(CategoryModel category)
    {
        try
        {
            return Ok(await _resourceService.Create(category));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutCategory(CategoryModel category)
    {
        try
        {
            await _resourceService.Update(category);
            return Ok();
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

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(CategoryModel category)
    {
        try
        {
            await _resourceService.Delete(category);
            return Ok();
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
}