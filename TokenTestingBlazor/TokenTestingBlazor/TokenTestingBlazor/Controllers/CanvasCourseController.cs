﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TokenTestingBlazor.Client.Models;

namespace TokenTestingBlazor.Controllers
{
    /// <summary>
    /// Controller class used for fetching Canvas course data.
    /// </summary>
    [Route("api/courses")]
    [ApiController]
    public class CanvasCourseController : ControllerBase
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Uri _endpoint = new Uri("https://davistech.instructure.com/api/v1/");

        /// <summary>
        /// Fetches all courses for the current user. [GET] /api/courses
        /// </summary>
        /// <param name="token">Canvas access token</param>
        /// <returns>List of Canvas courses</returns>
        [HttpGet]
        public async Task<ActionResult<List<CanvasCourse>>> GetAllCoursesAsync([FromHeader] string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await _client.GetAsync(_endpoint + "courses?include[]=total_students");
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return Ok(JsonSerializer.Deserialize<List<CanvasCourse>>(response.Content.ReadAsStream()));
        }

        /// <summary>
        /// Fetches all students for a course. [GET] /api/courses/{id}/students
        /// </summary>
        /// <param name="token">Canvas access token</param>
        /// <param name="id">Canvas course id</param>
        /// <returns>List of Canvas Students</returns>
        [HttpGet("{id}/students")]
        public async Task<ActionResult<List<CanvasStudent>>> GetStudentsInCourse([FromHeader] string token, int id)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await _client.GetAsync(_endpoint + $"courses/{id}/students");
            response.EnsureSuccessStatusCode();
            return Ok(JsonSerializer.Deserialize<List<CanvasStudent>>(response.Content.ReadAsStream()));
        }

        /// <summary>
        /// Fetches all assignments for a course. [GET] /api/courses/{id}/assignments
        /// </summary>
        /// <param name="token">Canvas access token</param>
        /// <param name="id">Canvas course id</param>
        /// <returns>List of Canvas Assignments</returns>
        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<List<CanvasAssignment>>> GetAssignmentsForCourse([FromHeader] string token, int id)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await _client.GetAsync(_endpoint + $"courses/{id}/assignments");
            response.EnsureSuccessStatusCode();
            return Ok(JsonSerializer.Deserialize<List<CanvasAssignment>>(response.Content.ReadAsStream()));
        }

        /// <summary>
        /// Fetches all modules for a course. [GET] /api/courses/{id}/modules
        /// </summary>
        /// <param name="token">Canvas access token</param>
        /// <param name="id">Canvas course id</param>
        /// <returns>List of Canvas course modules</returns>
        [HttpGet("{id}/modules")]
        public async Task<ActionResult<List<CanvasCourseModule>>> GetModulesForCourse([FromHeader] string token, int id)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await _client.GetAsync(_endpoint + $"courses/{id}/modules");
            response.EnsureSuccessStatusCode();
            return Ok(JsonSerializer.Deserialize<List<CanvasCourseModule>>(response.Content.ReadAsStream()));
        }
    }
}
