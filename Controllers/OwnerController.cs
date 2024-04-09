﻿using CarRental_BE.Models.PostVehicle;
using CarRental_BE.Repositories.PostVehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IPostVehicleRepository _postVehicleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OwnerController(IPostVehicleRepository postVehicleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postVehicleRepository = postVehicleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("get-all-post-vehicles")]
        public async Task<IActionResult> GetAllPostVehicles()
        {
            try
            {
                var postVehicles = await _postVehicleRepository.GetPostVehicles();
                return Ok(postVehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving post vehicles: {ex.Message}");
            }
        }

        [HttpGet("get-post-vehicle/{id}")]
        public async Task<IActionResult> GetPostVehicle(long id)
        {
            try
            {
                var postVehicle = await _postVehicleRepository.GetPostVehicle(id);

                if (postVehicle == null)
                    return NotFound("Post vehicle not found");

                return Ok(postVehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving post vehicle: {ex.Message}");
            }
        }



        [HttpPost("create-post/{userId}")]
        public async Task<IActionResult> AddPostVehicle(PostVehicleVM postVehicleVM, long userId)
        {
            try
            {
               
                await _postVehicleRepository.AddPostVehicle(postVehicleVM, userId);
                return Ok("Post vehicle added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding post vehicle: {ex.Message}");
            }
        }

        [HttpPut("update-post/{postId}")]
        public async Task<IActionResult> UpdatePostVehicle(long postId, [FromBody] UpdateVehicleVM modal)
        {
            try
            {
                await _postVehicleRepository.UpdatePostVehicle(postId, modal);
                return Ok("Post vehicle updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating post vehicle: {ex.Message}");
            }
        }

        [HttpDelete("delete-post/{postId}")]
        public async Task<IActionResult> DeletePostVehicle(long postId)
        {
            try
            {
                await _postVehicleRepository.DeletePostVehicle(postId);
                return Ok("Post vehicle deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting post vehicle: {ex.Message}");
            }
        }
    }
}
