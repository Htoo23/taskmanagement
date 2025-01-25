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
        public async Task<IActionResult> Index(string? search = null, string? status = null, string? priority = null)
        {
            var query = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(search)) query.Add("search", search);
            if (!string.IsNullOrEmpty(status)) query.Add("status", status);
            if (!string.IsNullOrEmpty(priority)) query.Add("priority", priority);

            var queryString = string.Join("&", query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

            var response = await _httpClient.GetAsync($"tasks?{queryString}");
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
                return BadRequest("Task ID mismatch");
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

        [HttpGet("{id}/progress")]
        public async Task<IActionResult> TrackProgress(int id)
        {
            var response = await _httpClient.GetAsync($"tasks/{id}/progress");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var progress = JsonSerializer.Deserialize<string>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Ok(new { TaskId = id, Progress = progress });
        }

        [HttpPatch("{id}/priority")]
        public async Task<IActionResult> AssignPriority(int id, [FromBody] string priorityLevel)
        {
            var json = JsonSerializer.Serialize(priorityLevel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"tasks/{id}/priority", content);
            response.EnsureSuccessStatusCode();
            return NoContent();
        }

        [HttpPatch("{id}/deadline")]
        public async Task<IActionResult> AssignDeadline(int id, [FromBody] string deadline)
        {
            var json = JsonSerializer.Serialize(deadline);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"tasks/{id}/deadline", content);
            response.EnsureSuccessStatusCode();
            return NoContent();
        }
    }
}
