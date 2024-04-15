﻿using CarRental_BE.Models.Auth;
using CarRental_BE.Models.User;
using CarRental_BE.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userRepository.Login(model);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            var responseData = new
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Role = user.Role
            };

            // Return response with user data and role
            return Ok(responseData);
        }

        [HttpPost("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle([FromQuery] string googleEmail)
        {
            var user = await _userRepository.LoginWithGoogleEmail(googleEmail);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            var responseData = new
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Role = user.Role
            };

            // Return response with user data and role
            return Ok(responseData);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginRequest request)
        {
            var user = await _userRepository.LoginWithGoogle(request.Token);

            if (user == null)
            {
                return BadRequest("Failed to login with Google");
            }

            var responseData = new
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Role = user.Role
            };

            return Ok(responseData);
        }
        #endregion Login

        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var success = await _userRepository.Register(model);

            if (!success)
            {
                return Conflict("User with this email already exists");
            }

            return Ok("Registration successful!");
        }
        #endregion Register


        #region ChangePassword
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            var success = await _userRepository.ChangePasswordUser(model);

            if (!success)
            {
                return BadRequest("Failed to change password. Please check your credentials.");
            }

            return Ok("Password changed successfully!");
        }
        #endregion ChangePassword

    }
}