using FirstApi.DTOs;
using FirstApi.Models;
using FirstApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    //GET /api/tasks
    //Récupère toutes les tâches (avec filtres, tri, pagination)
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool? isDone,
        [FromQuery] string? title,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sort = "id")
    {
        var tasks = await _taskService.GetAll(isDone, title, page, pageSize, sort);
        return Ok(tasks);
    }

    //GET /api/tasks/mine
    //Récupère uniquement les tâches de l'utilisateur connecté (via JWT)
    [Authorize]
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyTasks()
    {
        var tasks = await _taskService.GetByUser(GetUserId());

        return Ok(tasks);
    }

    //POST /api/tasks
    //Crée une nouvelle tâche pour l'utilisateur connecté
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add(CreateTaskDto entry)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var task = await _taskService.Add(entry, GetUserId());

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    //GET /api/tasks/{id}
    //Récupère une tâche par son ID
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetById(id, GetUserId());

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    //PUT /api/tasks/{id}
    //Met à jour une tâche
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto entry)
    {
        var task = await _taskService.Update(id, entry.Title, entry.IsDone, GetUserId());

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    //DELETE /api/tasks/{id}
    //Supprime une tâche
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _taskService.Delete(id, GetUserId());

        if (!result)
            return NotFound();

        return NoContent();
    }

    //UserId récupéré depuis le token (sécurité)
    private int GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(claim, out int userId))
            throw new UnauthorizedAccessException();

        return userId;
    }
}