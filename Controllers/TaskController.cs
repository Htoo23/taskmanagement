using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskmanagementSystem.Core.Entities;

namespace TaskManagementApp.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class TaskController : ControllerBase 
    {
        private readonly HttpClient _httpClient;

        public TaskController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("tasks");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Ok(tasks); 
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task) 
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(task);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("tasks", content);
                response.EnsureSuccessStatusCode();
                return CreatedAtAction(nameof(Index), new { id = task.Id }, task);
            }
            return BadRequest(ModelState); 
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TaskItem task) 
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(task);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"tasks/{id}", content);
                response.EnsureSuccessStatusCode();
                return NoContent(); 
            }
            return BadRequest(ModelState); 
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var response = await _httpClient.DeleteAsync($"tasks/{id}");
            response.EnsureSuccessStatusCode();
            return NoContent(); 
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"tasks/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var task = JsonSerializer.Deserialize<TaskItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Ok(task); 
        }

        
        [HttpPatch("{id}/progress")]
        public async Task<IActionResult> UpdateProgress(int id, [FromBody] string progressStatus) 
        {
            var json = JsonSerializer.Serialize(progressStatus);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"tasks/{id}/progress", content);
            response.EnsureSuccessStatusCode();
            return NoContent(); 
        }
    }
}